using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int? PaymentAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int? RemainingPayment { get; set; }

        [Required]
        public int? TotalAmount { get; set; }

        // Foreign Key to Student
        [Required]
        public int? StudentId { get; set; }

        public Student? PaymentStudent { get; set; }

        // Foreign Key to Class
        [Required]
        public int? ClassId { get; set; }

        // Navigation Property
        public Class? PaymentClass { get; set; }
    }
}
