using AutoMapper;
using mediAPI.Extensions;
using mediAPI.Models;
using MediLast.Abstractions.Interfaces;
using MediLast.Dtos.Message;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace MediLast.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<Account> _userManager;

        public MessageHub(IMessageRepository messageRepository, IMapper mapper, UserManager<Account> userManager)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.GetUserName(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var messages = await _messageRepository.
                GetMessageThread(Context.User.GetUserName(), otherUser);

            await Clients.Group(groupName).SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var username = Context.User.GetUserName();

            if (username == createMessageDto.RecipientUsername.ToLower())
                throw new HubException("You cannot send messages to yourself");

            var sender = await _userManager.FindByNameAsync(username);
            var recipient = await _userManager.FindByNameAsync(createMessageDto.RecipientUsername);

            if (recipient == null) throw new HubException("User not found");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderId = sender.Id,
                SenderUsername = sender.UserName,
                RecipientId = recipient.Id,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            // Check If user added Image

            if (createMessageDto.ImageData != null)
            {
                //if (createMessageDto.ImageData.Length > 40)
                //{
                //    throw new HubException("Image size exceeds limit");
                //}

                message.ImageData = createMessageDto.ImageData;
            }

            _messageRepository.AddMessage(message);

            if (await _messageRepository.SaveMessage())
            {
                var groupName = GetGroupName(sender.UserName, recipient.UserName);
                await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
            }

        }

        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}