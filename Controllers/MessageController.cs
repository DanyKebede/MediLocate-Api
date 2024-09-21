using AutoMapper;
using mediAPI.Abstractions.Interfaces;
using mediAPI.Extensions;
using mediAPI.Models;
using MediLast.Abstractions.Interfaces;
using MediLast.Dtos.Message;
using MediLast.Extensions;
using MediLast.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediLast.Controllers
{
    // Message Test Controller
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;

        public MessageController(IAccountRepository accountRepository,
            IMessageRepository messageRepository,
            UserManager<Account> userManager,
            IMapper mapper
            )
        {
            _accountRepository = accountRepository;
            _messageRepository = messageRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpPost("CreateMessage")]
        public async Task<ActionResult<MessageDto>> createMessage([FromBody] CreateMessageDto createMessage)
        {
            var userId = Guid.Parse(User.GetUserId());
            var userName = User.GetUserName();
            if (userName == createMessage.RecipientUsername.ToLower())
                return BadRequest("You cannot send a message to yourself");

            var recipient = await _userManager.FindByNameAsync(createMessage.RecipientUsername);
            if (recipient == null)
                return NotFound();
            var sender = await _userManager.FindByNameAsync(userName);

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderId = sender.Id,
                SenderUsername = sender.UserName,
                RecipientId = recipient.Id,
                RecipientUsername = recipient.UserName,
                Content = createMessage.Content
            };

            _messageRepository.AddMessage(message);

            if (await _messageRepository.SaveMessage()) return Ok(_mapper.Map<MessageDto>(message));

            return BadRequest("Can't Send Message");

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery]
            MessageParams messageParams)
        {
            var userName = User.GetUserName();

            messageParams.Username = userName;

            var messages = await _messageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);

            return messages;
        }

        [HttpGet("thread/{userName}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> getMessageThread(string userName)
        {
            var currentUserName = User.GetUserName();

            var messages = await _messageRepository.GetMessageThread(currentUserName, userName);

            return Ok(messages);

        }
    }
}
