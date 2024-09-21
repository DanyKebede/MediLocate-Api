using AutoMapper;
using AutoMapper.QueryableExtensions;
using mediAPI.Data;
using mediAPI.Models;
using MediLast.Abstractions.Interfaces;
using MediLast.Dtos.Message;
using MediLast.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediLast.Abstractions.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MediDbContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(MediDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(Guid id)
        {
            return await _context.Messages.FindAsync(id);

        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
                .OrderByDescending(m => m.MessageSent)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username
                    ),
                "Outbox" => query.Where(u => u.SenderUsername == messageParams.Username),
                _ => query.Where(u => u.RecipientUsername == messageParams.Username)
            };

            return await PagedList<MessageDto>.CreateAsync(query, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName)
        {
            var messages = await _context.Messages
                     .Where(m => m.Recipient.UserName == currentUserName && m.Sender.UserName == recipientUserName
                     || m.Recipient.UserName == recipientUserName && m.Sender.UserName == currentUserName)
                    .OrderBy(m => m.MessageSent)
                    .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null && m.RecipientUsername == currentUserName).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<MessageDto>>(messages);

        }

        public async Task<bool> SaveMessage()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
