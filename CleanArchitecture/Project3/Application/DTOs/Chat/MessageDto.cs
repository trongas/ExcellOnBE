namespace Application.DTOs.Chat
{
    public class MessageDto
    {
        public string GroupName { get; set; }
        public string SenderUserName { get; set; }
        public string ReciveUserName { get; set; }
        public string Content { get; set; }
    }
}
