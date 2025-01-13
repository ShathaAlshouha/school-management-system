using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Student

    {

        [Key]

        public int StudentId { get; set; }



        [Required]

        public DateTime? EnrollmentDate { get; set; }



        [ForeignKey(nameof(StudentClass))]

        [Required]

        public int? ClassId { get; set; }

        public Class? StudentClass { get; set; }

        public List<Payment>? Payment { get; set; }



        [ForeignKey(nameof(StudentPerson))]

        [Required]

        public int? PersonId { get; set; }

        public Person? StudentPerson { get; set; }



        [ForeignKey(nameof(StudentParent))]

      

        public int? ParentId { get; set; }

        public Parent? StudentParent { get; set; }



        public List<Grade>? Grade { get; set; }









    }
}
