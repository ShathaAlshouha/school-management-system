using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Schedual

    {

        [Key]

        public int ScheduleId { get; set; }



        [Required]

        public string? DayOfWeek { get; set; }

        [Required]

        public TimeSpan StartTime { get; set; }

        [Required]

        public TimeSpan EndTime { get; set; }

        [Required]
        public int? RoomNumber { get; set; }

        [ForeignKey(nameof(SchedualTeachers))]

        [Required]

        public int TeacherId { get; set; }

        public Teacher? SchedualTeachers { get; set; }

        [ForeignKey(nameof(SchedualCourses))]

        [Required]

        public int? CourseId { get; set; }

        public Course? SchedualCourses { get; set; }



    }


}
