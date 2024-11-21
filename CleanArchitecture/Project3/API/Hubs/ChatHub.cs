using Application.DTOs.Chat;
using Domain.Abstraction;
using Domain.Entities;
using Infrastructure.Repository;
using Infrastructure.Services.Impl;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatGroupRepository _groupRepository;
        private readonly MessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ChatService _chatService;
        public ChatHub(
            ChatGroupRepository chatGroupRepository,
            MessageRepository messageRepository,
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            ChatService chatService
            )
        {
            _groupRepository = chatGroupRepository;
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _chatService = chatService;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("UserConnected");

        }
        public async Task CreatePrivateChat(GroupChatDto group)
        {
            if (group.EmployeeId != null && group.CustomerId != null)
            {
                string groupName = group.CustomerId + "-" + group.EmployeeId;
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                var employee = await _unitOfWork.Employees.GetById(group.EmployeeId);
                string employeeEmail = employee.Email;
                var employeeConnectionId = _chatService.GetConnectionIdByUser(employeeEmail);
                await Clients.Client(employeeConnectionId).SendAsync("OpenPrivateChat", groupName);
                Console.WriteLine("Employee connectionId :", employeeConnectionId);
            }
            else
            {
                Console.WriteLine("Group null ");
            }
        }

        public async Task AddUserConnectionId(string email)
        {
            Console.WriteLine($" Adding user : {email}");
            _chatService.AddUserConnectionId(email, Context.ConnectionId);
            Console.WriteLine($"User connection email: {email}");
        }
        public async Task RecivePrivateMessage(MessageDto message)
        {
            if (message != null)
            {
                var groupId = _groupRepository.GetIdByName(message.GroupName);
                var sender = await _userManager.FindByEmailAsync(message.SenderUserName);
                var reciver = await _userManager.FindByEmailAsync(message.ReciveUserName);
                if (sender != null && reciver != null)
                {
                    var newMessage = new Message()
                    {
                        Content = message.Content,
                        GroupId = groupId,
                        GroupName = message.GroupName,
                        ReceiveId = reciver.Id,
                        SenderId = sender.Id
                    };
                    await _messageRepository.InsertMessageAsync(newMessage);
                    await Clients.All.SendAsync("ReciveMessage", message);
                }
            }
        }



    }

}
