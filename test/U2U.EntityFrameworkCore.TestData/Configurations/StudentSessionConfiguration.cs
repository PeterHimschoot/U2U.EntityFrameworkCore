namespace U2U.EntityFrameworkCore.TestData.Configurations;

public class StudentSessionConfiguration : IEntityTypeConfiguration<StudentSession>
{
  public void Configure(EntityTypeBuilder<StudentSession> builder)
  {
    builder
      .HasKey(ss => new { ss.SessionId, ss.StudentId });
    builder
      .HasOne(ss => ss.Session)
      .WithMany(s => s.Students)
      .HasForeignKey(ss => ss.SessionId);
    builder
      .HasOne(ss => ss.Student)
      .WithMany(s => s.Sessions)
      .HasForeignKey(ss => ss.StudentId);
  }
}
