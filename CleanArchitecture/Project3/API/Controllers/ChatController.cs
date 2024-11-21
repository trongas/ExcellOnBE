using Application.DTOs.Chat;
using Domain.Abstraction;
using Domain.Entities;
using Infrastructure.Repository;
using Infrastructure.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatGroupRepository _chatGroupRepository;
        private readonly MessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public ChatController(ChatGroupRepository chatGroupRepository, MessageRepository messageRepository, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _chatGroupRepository = chatGroupRepository;
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }

        [HttpGet]
        [Route("GetAllGroupByEmployeeId")]
        public async Task<IActionResult> GetAllGroupByEmployeeId(int employeeId)
        {
            var groups = await _chatGroupRepository.GetGroupByEmployeeId(employeeId);
            List<GroupChatDto> result = new List<GroupChatDto>();
            foreach (var group in groups)
            {
                if (group != null)
                {
                    var employee = await _unitOfWork.Employees.GetById(group.EmployeeId);
                    var client = await _unitOfWork.Clients.GetById(group.ClientId);
                    if (employee != null && client != null)
                    {
                        var chatGroupDto = new GroupChatDto()
                        {
                            GroupId = group.Id,
                            GroupName = group.Name,
                            CustomerId = client.ClientId,
                            CustomerName = client.FirstName + " " + client.LastName,
                            EmployeeId = employeeId,
                            EmployeeName = employee.FirstName + " " + employee.LastName,
                            EmployeeEmail = employee.Email,
                            CustomerEmail = client.Email
                        };
                        result.Add(chatGroupDto);
                    }
                }
            }
            return
                Ok(result);
        }
        [HttpPost]
        [Route("CreateGroupChat")]
        public async Task<IActionResult> CreateGroupChat(GroupChatDto group)
        {
            if (group != null)
            {
                string groupName = group.CustomerId + "-" + group.EmployeeId;
                var newGroup = new ChatGroup()
                {
                    ClientId = group.CustomerId,
                    EmployeeId = group.EmployeeId,
                    Name = groupName
                };
                await _chatGroupRepository.InsertChatGroupAsync(newGroup);
                return Ok(newGroup);
            }
            return BadRequest("Group can't null");
        }

        [HttpGet]
        [Route("GetGroupByClientId")]
        public async Task<IActionResult> GetGroupByClientId(int clientId)
        {
            var group = _chatGroupRepository.GetGroupByCustomerId(clientId);



            if (group != null)
            {
                var employee = await _unitOfWork.Employees.GetById(group.EmployeeId);
                var client = await _unitOfWork.Clients.GetById(group.ClientId);
                if (employee != null && client != null)
                {
                    var chatGroupDto = new GroupChatDto()
                    {
                        GroupName = group.Name,
                        CustomerId = clientId,
                        CustomerName = client.FirstName + " " + client.LastName,
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.FirstName + " " + employee.LastName,
                        EmployeeEmail = employee.Email,
                        CustomerEmail = client.Email,
                    };

                    return Ok(chatGroupDto);
                }
            }
            return BadRequest("Group not found");
        }

        [HttpGet]
        [Route("GetAllMessageByGroupName")]
        public async Task<IActionResult> GetAllMessageByGroupName(string groupName)
        {
            string groupId = _chatGroupRepository.GetIdByName(groupName);
            var messages = await _messageRepository.GetAllByGroupId(groupId);
            List<MessageDto> messagesDto = new List<MessageDto>();
            foreach (var message in messages)
            {
                if (message != null)
                {
                    var reciver = await _userManager.FindByIdAsync(message.ReceiveId);
                    var sender = await _userManager.FindByIdAsync(message.SenderId);
                    if (reciver != null && sender != null)
                    {
                        var newMessageDto = new MessageDto()
                        {
                            GroupName = groupName,
                            Content = message.Content,
                            ReciveUserName = reciver.UserName,
                            SenderUserName = sender.UserName
                        };
                        messagesDto.Add(newMessageDto);
                    }
                }
            }
            return Ok(messagesDto);
        }
    }
}
