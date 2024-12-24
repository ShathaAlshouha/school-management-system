using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class CourseDto
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string? CourseName { get; set; }
        public int? TeacherId { get; set; }
      
        public int? ClassId { get; set; }
        public Class? CourseClass { get; set; }
   
    }
}
