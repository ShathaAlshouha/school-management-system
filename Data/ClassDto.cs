using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class ClassDto
    {
        [Key]
        public int ClassId { get; set; }
        [Required]
        public string? ClassName { get; set; }
        [Required]
        public int? fees { get; set; }
        [Key]
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}
