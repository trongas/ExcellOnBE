namespace Application.DTOs.Payment
{
    public class ConfirmPaymentDto
    {
        public string TokenId { get; set; }
        public string ClientSecret { get; set; }
    }
}
