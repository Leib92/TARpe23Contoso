using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        public string FullName 
        {  
            get {  return LastName + ", " + FirstName; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime HireDate { get; set; }

        public ICollection<CourseAssignment>? CourseAssignments { get; set; }

        public OfficeAssignment? OfficeAssignment { get; set; }
        
        // made 3 stuff up below

        [StringLength(50)]
        public string? MagicLore { get; set; }

        public Student? Apprentice { get; set; }

        public int? DeathCount { get; set; }


    }
}
