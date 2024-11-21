using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repository
{
    public class ChatGroupRepository : IChatGroupRepository
    {
        private readonly IMongoCollection<ChatGroup> _chatGroupCollection;

        public ChatGroupRepository(IOptions<ChatDatabaseSettings> chatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            chatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                chatDatabaseSettings.Value.DatabaseName);
            _chatGroupCollection = mongoDatabase.GetCollection<ChatGroup>("ChatGroups");
        }

        public async Task<IEnumerable<ChatGroup>> All()
        {
            try
            {
                var chatGroups = await _chatGroupCollection.Find(_ => true).ToListAsync();
                return chatGroups;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving chat groups: {ex.Message}");
                return null;
            }
        }

        public string GetIdByName(string groupName)
        {
            var group = _chatGroupCollection.AsQueryable().FirstOrDefault(group => group.Name == groupName);
            if (group == null)
            {
                Console.WriteLine($"{groupName} is not a chat group");
                return null;
            }
            return group.Id.ToString();
        }


        public async Task InsertChatGroupAsync(ChatGroup chatGroup)
        {
            try
            {
                var existingGroup = _chatGroupCollection.AsQueryable().Where(group => group.Name == chatGroup.Name).FirstOrDefault();
                Console.WriteLine("Chat Repo ");

                if (existingGroup == null)
                {
                    await _chatGroupCollection.InsertOneAsync(chatGroup);
                }
                else
                {
                    Console.WriteLine("Group already exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while inserting chat group: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ChatGroup>> GetGroupByEmployeeId(int employeeId)
        {
            try
            {
                // Tìm kiếm các nhóm mà nhân viên có ID được cung cấp là thành viên
                var chatGroups = await _chatGroupCollection.Find(group => group.EmployeeId.Equals(employeeId)).ToListAsync();

                return chatGroups;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving chat groups by employee ID: {ex.Message}");
                return null;
            }
        }

        public ChatGroup GetGroupByCustomerId(int customerId)
        {
            try
            {
                // Tìm kiếm các nhóm mà khách hàng có ID được cung cấp là thành viên
                var chatGroup = _chatGroupCollection.Find(group => group.ClientId.Equals(customerId)).FirstOrDefault();
                if (chatGroup != null)
                {
                    return chatGroup;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving chat groups by customer ID: {ex.Message}");
                return null;
            }
        }
    }
}
