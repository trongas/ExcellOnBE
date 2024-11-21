using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Chat
{
    public class GroupChatDto
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string CustomerEmail { get; set; }
    }
}
