using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }

        [StringLength(50)]
        [Display(Name = "Location of the Office")]
        public string Location { get; set; }
    }
}
