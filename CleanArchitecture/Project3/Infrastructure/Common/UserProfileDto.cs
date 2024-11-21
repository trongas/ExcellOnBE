﻿namespace Infrastructure.Common
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Dob { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string UserAspId { get; set; }
    }
}
