using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace U2U.EntityFrameworkCore.TestData
{
  public class TrainingDb : DbContext
  {
    public DbSet<Course> Courses { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Login> Logins { get; set; }

    public TrainingDb(DbContextOptions<TrainingDb> options)
      : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      const int courseCodeMaxLength = 10;

      modelBuilder.Entity<Course>()
         .HasKey(c => c.Id);
      modelBuilder.Entity<Course>()
         .Property(c => c.Code)
         .HasMaxLength(courseCodeMaxLength);
      modelBuilder.Entity<Course>()
        .HasData(
          new Course(1) { Code = "UARCH", Name = "Patterns and Practices" },
          new Course(2) { Code = "UWEBA", Name = "Advanced Web Development" },
          new Course(3) { Code = "UCORE", Name = "Upgrade to DotNet Core" },
          new Course(4) { Code = "UDEF", Name = "Domain Driven Design With Entity Framework Core" }
          );

      modelBuilder.Entity<Session>()
       .HasKey(s => s.Id);
      modelBuilder.Entity<Session>()
        .HasOne(s => s.Course)
        .WithMany(c => c.Sessions)
        .HasForeignKey(s => s.CourseId);

      modelBuilder.Entity<Session>()
        .HasData(
          new Session(1) { CourseId = 1, StartDate = new DateTime(2020, 1, 6), EndDate = new DateTime(2020, 1, 10) },
          new Session(2) { CourseId = 2, StartDate = new DateTime(2020, 1, 6), EndDate = new DateTime(2020, 1, 10) },
          new Session(3) { CourseId = 3, StartDate = new DateTime(2020, 1, 6), EndDate = new DateTime(2020, 1, 10) },
          new Session(4) { CourseId = 1, StartDate = new DateTime(2020, 2, 6), EndDate = new DateTime(2020, 2, 10) },
          new Session(5) { CourseId = 2, StartDate = new DateTime(2020, 3, 6), EndDate = new DateTime(2020, 3, 10) },
          new Session(6) { CourseId = 3, StartDate = new DateTime(2020, 4, 6), EndDate = new DateTime(2020, 4, 10) },
          new Session(7) { CourseId = 1, StartDate = new DateTime(2020, 5, 6), EndDate = new DateTime(2020, 5, 10) },
          new Session(8) { CourseId = 2, StartDate = new DateTime(2020, 6, 6), EndDate = new DateTime(2020, 6, 10) },
          new Session(9) { CourseId = 3, StartDate = new DateTime(2020, 7, 6), EndDate = new DateTime(2020, 7, 10) }
        );

      modelBuilder.Entity<Student>()
        .HasKey(st => st.Id);

      modelBuilder.Entity<Student>()
        .HasOne(s => s.Login)
        .WithOne(l => l.Student)
        .HasForeignKey<Login>(l => l.StudentId);

      modelBuilder.Entity<Student>()
        .HasData(
          new Student(1) { FirstName = "Joske", LastName = "Vermeulen" },
          new Student(2) { FirstName = "Eddy", LastName = "Wally" },
          new Student(3) { FirstName = "Sam", LastName = "Goris" }
        );

      modelBuilder.Entity<Login>()
        .HasKey(l => l.Id);

      modelBuilder.Entity<Login>()
        .HasData(
          new Login(1) { Provider = "X", StudentId = 1 },
          new Login(2) { Provider = "X", StudentId = 2 },
          new Login(3) { Provider = "X", StudentId = 3 }
        );

      modelBuilder.Entity<StudentSession>()
        .HasKey(ss => new { ss.SessionId, ss.StudentId });
      modelBuilder.Entity<StudentSession>()
        .HasOne(ss => ss.Session)
        .WithMany(s => s.Students)
        .HasForeignKey(ss => ss.SessionId);
      modelBuilder.Entity<StudentSession>()
        .HasOne(ss => ss.Student)
        .WithMany(s => s.Sessions)
        .HasForeignKey(ss => ss.StudentId);

      modelBuilder.Entity<StudentSession>()
        .HasData(
          new StudentSession() { SessionId = 1, StudentId = 1 },
          new StudentSession() { SessionId = 1, StudentId = 3 },
          new StudentSession() { SessionId = 2, StudentId = 2 },
          new StudentSession() { SessionId = 3, StudentId = 1 },
          new StudentSession() { SessionId = 3, StudentId = 2 },
          new StudentSession() { SessionId = 3, StudentId = 3 }
        );
    }
  }
}
