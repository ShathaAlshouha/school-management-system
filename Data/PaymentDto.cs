using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class PaymentDto
    {
        public int? ClassId { get; set; }
        public int StudentId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PaymentAmount { get; set; }
        public int? TotalAmount { get; set; }
        public int? RemainingPayment { get; set; }
    }
}
