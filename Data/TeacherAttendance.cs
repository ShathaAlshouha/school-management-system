using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class TeacherAttendance

    {

        [Key]

        public int AttendanceId { get; set; }

        [Required]

        public DateTime? date { get; set; }

        [Required]

        public string? status { get; set; }



        [ForeignKey(nameof(Teacher))]

        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }

    }
}
