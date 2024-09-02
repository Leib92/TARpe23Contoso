﻿using System.ComponentModel.DataAnnotations;

namespace ConstosoUniversity.Models
{
    public class Student
    {
        [Key] //primary key
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
