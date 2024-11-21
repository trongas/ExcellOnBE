using Amazon.Runtime.Internal.Util;
using CloudinaryDotNet.Actions;
using Infrastructure.Helpter;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.ForgotPassword.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandle : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _cache;

        public ForgotPasswordCommandHandle(IMediator mediator, IEmailService emailService, IMemoryCache cache)
        {
            _mediator = mediator;
            _emailService = emailService;
            _cache = cache;
        }
        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var otp = GenerateOTP();

            // Send email with OTP
            var emailRequest = new EmailRequest
            {
                ToEmail = request.Email,
                Subject = "Password Reset OTP",
                Body = $"Your OTP for password reset is: {otp}. Please enter this code to reset your password."
            };
            await _emailService.SendEmailAsync(emailRequest);

            // Cache OTP with user's email as key
            _cache.Set(request.Email, otp, TimeSpan.FromMinutes(10)); // Cache for 10 minutes

            return Unit.Value;
        }
        private string GenerateOTP()
        {
            Random rnd = new Random();
            int otp = rnd.Next(100000, 999999);
            return otp.ToString();
        }
        private string HashOTP(string otp)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the OTP to bytes
                byte[] otpBytes = Encoding.UTF8.GetBytes(otp);
                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(otpBytes);
                // Convert the hash to string
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }
}
