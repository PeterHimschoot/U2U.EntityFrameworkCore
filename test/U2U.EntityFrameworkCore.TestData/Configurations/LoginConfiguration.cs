namespace U2U.EntityFrameworkCore.TestData.Configurations;

public class LoginConfiguration : IEntityTypeConfiguration<Login>
{
  public void Configure(EntityTypeBuilder<Login> builder) 
    => builder
      .HasKey(l => l.Id);
}

