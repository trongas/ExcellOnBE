using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task InsertMessageAsync(Message message);
        Task<IEnumerable<Message>> GetAllByGroupId(string groupId);
    }
}
