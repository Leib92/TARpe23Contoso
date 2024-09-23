using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }
        public Instructor? Mood { get; set; }
        [StringLength(50)]
        public string VocationCredientials { get; set; }
        public int WorkYears { get; set; }


    }
}
