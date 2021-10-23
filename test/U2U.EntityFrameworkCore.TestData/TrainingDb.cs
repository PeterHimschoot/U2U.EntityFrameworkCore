
namespace U2U.EntityFrameworkCore.TestData;

public class TrainingDb : DbContext
{
  public DbSet<Session> Courses { get; set; } = default!;
  public DbSet<Session> Sessions { get; set; } = default!;
  public DbSet<Student> Students { get; set; } = default!;
  public DbSet<Login> Logins { get; set; } = default!;

  public TrainingDb(DbContextOptions<TrainingDb> options)
    : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfiguration(new CourseConfiguration());
    modelBuilder.ApplyConfiguration(new SessionConfiguration());
    modelBuilder.ApplyConfiguration(new StudentConfiguration());
    modelBuilder.ApplyConfiguration(new StudentSessionConfiguration());
    modelBuilder.ApplyConfiguration(new LoginConfiguration());

    modelBuilder.ApplyTestData(new CourseTestData());
    modelBuilder.ApplyTestData(new SessionTestData());
    modelBuilder.ApplyTestData(new StudentTestData());
    modelBuilder.ApplyTestData(new StudentSessionTestData());
    modelBuilder.ApplyTestData(new LoginTestData());
  }
}

