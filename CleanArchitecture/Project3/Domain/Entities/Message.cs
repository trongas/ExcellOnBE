using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.Entities
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        //Take in Identity Table
        public string SenderId { get; set; }
        //Take in Identity Table
        public string ReceiveId { get; set; }
        public string Content { get; set; }

    }
}
