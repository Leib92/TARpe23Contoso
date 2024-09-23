namespace ContosoUniversity.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }



        public Student? ForeignStudent { get; set; }
        public int? SkeletonsSummoned { get; set; }
        public int? InstructorID { get; set; }
        public byte? RowVersion { get; set; }
        public Instructor? Administrator { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
