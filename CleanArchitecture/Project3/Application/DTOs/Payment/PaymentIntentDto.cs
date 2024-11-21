namespace Application.DTOs.Payment
{
    public class PaymentIntentDto
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public int ClientId { get; set; }
    }
}
