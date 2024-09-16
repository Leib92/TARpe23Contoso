using ContosoUniversity.Controllers;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class CourseAssignment
    {
        [Key]
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public int CourseId { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}
