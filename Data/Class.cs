using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Class

    {

        [Key]
        public int ClassId { get; set; }
        [Required]
        public string? ClassName { get; set; }
        [Required]
        public int? fees { get; set; }
        [ForeignKey(nameof(ClassTeacher))]

        [Required]

        public int? TeacherId { get; set; }

        public Teacher? ClassTeacher { get; set; }

        public List<Student>? Students { get; set; }

        public List<Course>? courses { get; set; }

        public Payment? Payment { get; set; }



    }
}
