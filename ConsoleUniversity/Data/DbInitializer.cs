using ConsoleUniversity.Models;

namespace ConsoleUniversity.Data
{
    public class DbInitializer
    {
        public static void Initializer(SchoolContext context) 
        { 
            context.Database.EnsureCreated();

            if  (context.Students.Any()) 
            {
                return;
            }

            var students = new Student[] 
            {
                new Student {FirstName = "Artjom", LastName = "Skatskov", EnrollmentDate=DateTime.Parse("2069-04-20")}
            };
        }

    }
}
