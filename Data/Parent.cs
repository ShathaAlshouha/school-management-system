using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data
{
    public class Parent

    {

        [Key]

        public int ParentID { get; set; }

        [Required]

        public string? RelationshipToStudent { get; set; }

        [ForeignKey(nameof(ParentPerson))]

        public int PersonID { get; set; }

        public Person? ParentPerson { get; set; }

        public List<Student>? ParentStudent { get; set; }



    }
}
