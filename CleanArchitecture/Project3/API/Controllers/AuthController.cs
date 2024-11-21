using Amazon.Runtime.Internal.Util;
using Application.Client.Commands.CreateClient;
using Application.DTOs.Auth;
using Application.DTOs.Chat;
using Application.DTOs.Client;
using Application.DTOs.Employee;
using Application.DTOs.Response.AuthResponse;
using Application.Employee.Commands.CreateEmployee;
using Application.ForgotPassword.Commands.ForgotPassword;
using Azure.Core;
using CloudinaryDotNet.Actions;
using Domain.Abstraction;
using Domain.Entities;
using Infrastructure.Repository;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly ChatGroupRepository _chatGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;


        public AuthController(IUnitOfWork unitOfWork, IMediator mediator, UserManager<IdentityUser> userManager, IJwtService jwtService,ChatGroupRepository chatGroupRepository, IMemoryCache cache)
        {
            _mediator = mediator;
            _userManager = userManager;
            _jwtService = jwtService;
            _chatGroupRepository = chatGroupRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        [HttpPost]
        [Route("GetEmployeeProfileByJwt")]
        public async Task<IActionResult> GetProfileByJwt([FromQuery] string token)
        {
            var userProfile = await _jwtService.GetProfileByJwt(token);

            return Ok(new { UserProfile = userProfile });
        }

        [HttpPost]
        [Route("EmployeeRegister")]
        public async Task<IActionResult> EmployeeRegister([FromForm] CreateEmployeeDto request)
        {
            if (ModelState.IsValid)
            {
                var userExiting = await _userManager.FindByEmailAsync(request.Email);
                if (userExiting != null)
                {
                    return BadRequest("Email is already exits !");
                }

                var newUser = new IdentityUser()
                {
                    Email = request.Email,
                    UserName = request.Email
                };

                var isCreate = await _userManager.CreateAsync(newUser, request.Password);

                if (isCreate.Succeeded)
                {
                    var createEmployeeCommand = new CreateEmployeeCommand
                    {
                        EmployeeDto = request,

                    };
                    var isAddRole = await _userManager.AddToRoleAsync(newUser, "Employee");
                    if (isAddRole.Succeeded)
                    {
                        await _mediator.Send(createEmployeeCommand);
                        var token = await _jwtService.GenerateJwtTokenAsync(newUser);
                        return Ok(
                            new EmployeeRegisterResponse()
                            {
                                Result = true,
                                Token = token.ToString()
                            }
                        );
                    }
                    return BadRequest(isAddRole.Errors.Select(x => x.Description).ToList());
                }
                return BadRequest(isCreate.Errors.Select(x => x.Description).ToList());
            }
            return BadRequest("Invalid request payload");
        }

        [HttpPost]
        [Route("client-register")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto request)
        {
            if (ModelState.IsValid)
            {
                var userExiting = await _userManager.FindByEmailAsync(request.Email);
                if (userExiting != null)
                {
                    return BadRequest("Email is already exits !");
                }

                var newUser = new IdentityUser()
                {
                    Email = request.Email,
                    UserName = request.Email
                };

                var isCreate = await _userManager.CreateAsync(newUser, request.Password);

                if (isCreate.Succeeded)
                {
                    var createClientCommand = new CreateClientCommand
                    {
                        ClientDto = request,

                    };
                    var isAddRole = await _userManager.AddToRoleAsync(newUser, "User");
                    if (isAddRole.Succeeded)
                    {
                        await _mediator.Send(createClientCommand);
                        var token = await _jwtService.GenerateJwtTokenAsync(newUser);
                        return Ok(
                            new EmployeeRegisterResponse()
                            {
                                Result = true,
                                Token = token.ToString()
                            }
                        );
                    }
                    return BadRequest(isAddRole.Errors.Select(x => x.Description).ToList());
                }
                return BadRequest(isCreate.Errors.Select(x => x.Description).ToList());
            }
            return BadRequest("Invalid request payload");

        }


        [HttpPost]
        [Route("Emp-Login")]
        public async Task<IActionResult> EmpLogin([FromBody] LoginRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser == null)
                {
                    return BadRequest("Invalid authentication");
                }
                var roles = await _userManager.GetRolesAsync(existingUser);
                if (!roles.Contains("Admin") && !roles.Contains("Employee"))
                {
                    return BadRequest("You don't have the required roles (Admin or Employee) to perform this action.");
                }
                var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, request.Password);
                if (isPasswordValid)
                {
                    var token = await _jwtService.GenerateJwtTokenAsync(existingUser);
                    return Ok(new LoginResponseDto()
                    {
                        Result = true,
                        Token = token.ToString()
                    }); ;
                }
            }
            return BadRequest("Invalid request payload !");
        }
        [HttpPost]
        [Route("client-login")]
        public async Task<IActionResult> ClientLogin([FromBody] LoginRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser == null)
                {
                    return BadRequest("Invalid authentication");
                }
                var roles = await _userManager.GetRolesAsync(existingUser);
                if (!roles.Contains("Admin") && !roles.Contains("User"))
                {
                    return BadRequest("You don't have the required roles (Admin or Customer) to perform this action.");
                }
                var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, request.Password);
                if (isPasswordValid)
                {
                    var token = await _jwtService.GenerateJwtTokenAsync(existingUser);
                    Random number = new Random();
                    int totalEmployee = await _unitOfWork.Employees.Count();
                    var randomEmpId = number.Next(1, totalEmployee);
                    var client = await _unitOfWork.Clients.GetClientByEmail(existingUser.Email);
                    var groupName = randomEmpId+ "-" +existingUser.Id.ToString();
                    string exittingGroup =  _chatGroupRepository.GetIdByName(groupName);
                    if(exittingGroup == null)
                    {
                        var newGroup = new ChatGroup()
                        {
                            Name = groupName,
                            ClientId = client.ClientId,
                            EmployeeId = randomEmpId
                        };
                        await _chatGroupRepository.InsertChatGroupAsync(newGroup);
                    }
                    return Ok(new LoginResponseDto()
                    {
                        Result = true,
                        Token = token.ToString()
                    }); ;
                }
            }
            return BadRequest("Invalid request payload !");
        }

        [HttpPost]
        [Route("send-mail")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _mediator.Send(command);
                return Ok("Password reset email sent successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost("confirm-otp")]
        public async Task<IActionResult> ConfirmOTP([FromBody] OTPConfirmationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_cache.TryGetValue(model.Email, out string storedOTP))
            {
                if (model.OTP == storedOTP)
                {
                    // OTP is confirmed, generate a new JWT token
                    var existingUser = await _userManager.FindByEmailAsync(model.Email);
                    if (existingUser != null)
                    {
                        var token = await _jwtService.GenerateJwtTokenAsync(existingUser);
                        return Ok(new LoginResponseDto()
                        {
                            Result = true,
                            Token = token.ToString()
                        });
                    }
                    else
                    {
                        return BadRequest("User not found.");
                    }
                }
            }

            // OTP is not valid or expired
            return BadRequest("Invalid or expired OTP");
        }



    }
}
