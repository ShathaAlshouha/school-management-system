using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class PaymentDto
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int? PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        [Required]
        public int? RemainingPayment { get; set; }
    }
}
