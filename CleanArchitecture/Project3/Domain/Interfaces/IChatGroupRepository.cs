using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChatGroupRepository
    {
        Task InsertChatGroupAsync(ChatGroup chatGroup);
        Task<IEnumerable<ChatGroup>> All();
        string GetIdByName(string groupName);
    }
}
