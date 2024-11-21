using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<Message> _messageCollection;
        public MessageRepository(IOptions<ChatDatabaseSettings> chatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            chatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                chatDatabaseSettings.Value.DatabaseName);
            _messageCollection = mongoDatabase.GetCollection<Message>("Message");
        }

        public async Task<IEnumerable<Message>> GetAllByGroupId(string groupId)
        {
            try
            {
                var messages = await _messageCollection.Find(message => message.GroupId == groupId).ToListAsync();
                return messages;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving messages by group ID: {ex.Message}");
                return null;
            }
        }

        public async Task InsertMessageAsync(Message message)
        {
            await _messageCollection.InsertOneAsync(message);
        }
    }
}
