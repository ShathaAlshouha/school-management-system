using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class User
    {

        [Key]

        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


        public DateTime CreatedOnDate { get; set; }

        [ForeignKey(nameof(UserPerson))]

        [Required]

        public int? PersonId { get; set; }

        public Person? UserPerson { get; set; }

    }


}
