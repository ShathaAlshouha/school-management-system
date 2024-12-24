using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Payment

    {

        [Key]

        public int PaymentId { get; set; }
        [Required]

        public int? PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }

        [Required]
        public int? RemainingPayment { get; set; }
        [Required]
        public int? TotalAmount { get; set; }

        [ForeignKey(nameof(PaymentStudent))]

        [Required]

        public int? StudentId { get; set; }

        public Student? PaymentStudent { get; set; }



        [ForeignKey(nameof(PaymentClass))]

        [Required]

        public int ClassId { get; set; }

        public Class? PaymentClass { get; set; }

    }


}
