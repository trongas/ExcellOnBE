using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class OTPConfirmationModel
    {
        public string OTP { get; set; }
        public string Email { get; set; }
    }
}
