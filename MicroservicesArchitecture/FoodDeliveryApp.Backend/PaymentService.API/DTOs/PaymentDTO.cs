using System.ComponentModel.DataAnnotations;

namespace PaymentService.API.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int UserId { get; set; }

        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Choose payment method")]
        public string? Method { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
