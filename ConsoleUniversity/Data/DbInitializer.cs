using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class DbInitializer
    {
        public static void Initializer(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student {FirstName = "Allan", LastName = "Leibenau", EnrollmentDate=DateTime.Parse("2075-02-06")},
                new Student {FirstName = "Sienna", LastName = "Fuegonasus", EnrollmentDate=DateTime.Parse("2069-04-20")},
                new Student {FirstName = "Meredith", LastName = "Alonso", EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student {FirstName = "John", LastName = "Smith", EnrollmentDate=DateTime.Parse("1984-11-11")},
                new Student {FirstName = "Bob", LastName = "Bobby", EnrollmentDate=DateTime.Parse("1910-12-30")},
                new Student {FirstName = "Jack", LastName = "Black", EnrollmentDate=DateTime.Parse("2045-02-05")},
                new Student {FirstName = "Godric", LastName = "Unicorn", EnrollmentDate=DateTime.Parse("1991-06-23")}
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseID = 1050, Title = "Chemistry", Credits = 3},
                new Course{CourseID = 4022, Title = "Micro-economics", Credits = 3},
                new Course{CourseID = 4041, Title = "Micro-economics", Credits = 3},
                new Course{CourseID = 1045, Title = "Calculus", Credits = 4},
                new Course{CourseID = 3141, Title = "Trigonometry", Credits = 4},
                new Course{CourseID = 2021, Title = "Composition", Credits = 3},
                new Course{CourseID = 2042, Title = "Literature", Credits = 4},
                new Course{CourseID = 1944, Title = "Responsible Spending", Credits = 1}
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            if (context.Enrollments.Any()) { return; }
            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID = 1, CourseID = 1050, Grade = Grade.A },
                new Enrollment{StudentID = 1, CourseID = 1944, Grade = Grade.B },
                new Enrollment{StudentID = 1, CourseID = 4041, Grade = Grade.C},

                new Enrollment{StudentID = 2, CourseID = 1050, Grade = Grade.F },
                new Enrollment{StudentID = 2, CourseID = 2042, Grade = Grade.C },
                new Enrollment{StudentID = 2, CourseID = 1045, Grade = Grade.A },

                new Enrollment{StudentID = 3, CourseID = 2021, Grade = Grade.A },

                new Enrollment{StudentID = 4, CourseID = 3141, Grade = Grade.B },
                new Enrollment{StudentID = 4, CourseID = 2021, Grade = Grade.C },

                new Enrollment{StudentID = 5, CourseID = 4041, Grade = Grade.C },

                new Enrollment{StudentID = 6, CourseID = 2021, Grade = Grade.A },

                new Enrollment{StudentID = 7, CourseID = 1944, Grade = Grade.B }
            };
            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();

            if (context.Instructors.Any()) { return; }
            var instructors = new Instructor[]
            {
                new Instructor
                {
                    FirstName = "Karl",
                    LastName = "Markson",
                    HireDate = DateTime.Parse("2003-09-01")
                }
            };
            context.Instructors.AddRange(instructors);
            context.SaveChanges();

            if (context.Departments.Any()) { return; }
            var departments = new Department[]
            {
                new Department
                {
                    Name = "InfoTechnology",
                    Budget = 0,
                    StartDate = DateTime.Parse("2024-09-01"),
                    SkeletonsSummoned = 0, // normies
                    InstructorID = 1
                },

                new Department
                {
                    Name = "LICHDOM",
                    Budget = 0,
                    StartDate = DateTime.Parse("2024-09-01"),
                    SkeletonsSummoned = 5318008, // we are screwed guys
                    InstructorID = 2
                },

                new Department
                {
                    Name = "Pathfinder 2e",
                    Budget = 0,
                    StartDate = DateTime.Parse("2024-09-01"),
                    SkeletonsSummoned = 2, // i have no dice and i must roll
                    InstructorID = 3
                }
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();

        }

    }
}
