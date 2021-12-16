using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DatabaseConsole
{
    // Entity Framework ("EF")

    public class Student // This class effectively functions as a table.
    {
        public int StudentId { get; set; } // It's basically a requirement to add the { get; set; } when using the Entity Framework ("EF")
        public string Name { get; set; } // I believe when these have getters/setters they are referred to as properties.
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    public class SchoolContext : DbContext // I believe the DbContext/DbSet allows SchoolContext to inherit fromthe Student/Course classes
    {
        public DbSet<Student> Students { get; set; } //DbSet is essentially a list
        public DbSet<Course> Courses { get; set; } //DbSet is essentially a list
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=efconsole1;User Id=sa;Password=abc123;");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("What student would you like to add?");
            // string entry = Console.ReadLine();

            // We need to create a new instance of the SchoolContext class
            using (var context = new SchoolContext()) // Doing this inside a "using statement", it opens a new connection to the database and eventually closes it. The "using" part includes a "dispose" at the end to close the connection. It basically just makes our job easier.
                                                      // Alternatively, instead of var, I could type the following instead: using (SchoolContext context = new SchoolContext()) {...}
                                                      // Within our code here, "context" represents our database. It's essentailly our connection to it.
            {
                // Student st = new Student() { Name = entry }; // StudentId doesn't need to be included because it's autoincremented (database knows to do this)
                // context.Students.Add(st);
                // context.SaveChanges(); // Saves the changes to the database (in other words, it does an insert). Nothing will happen until you add this line.


                // Student st2 = new Student() { Name = "Paul" };
                // context.Students.Add(st2);

                // Student st3 = new Student() { StudentId = 1, Name = "Mary" };
                // context.Students.Update(st3);

                Student st4 = new Student() { StudentId = 2 }; // This will remove the second student from the exisiting database. Should be Fred.
                context.Students.Remove(st4);

                context.SaveChanges();




            } // After it reaches this point, the connection to the database is broken. I THINK he also said the instance of Student st is gone once the connection is closed.

            Console.WriteLine("All done!");
        }
    }
}
