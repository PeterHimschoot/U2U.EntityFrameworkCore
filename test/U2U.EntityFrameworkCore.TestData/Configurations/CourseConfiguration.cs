namespace U2U.EntityFrameworkCore.TestData.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
  public const int CourseCodeMaxLength = 10;

  public void Configure(EntityTypeBuilder<Course> builder)
  {
    builder
      .HasKey(c => c.Id);
    builder
      .Property(c => c.Code)
      .HasMaxLength(CourseCodeMaxLength);
  }
}

