namespace U2U.EntityFrameworkCore.Abstractions.Interfaces;

public interface IEntityTypeData<T> where T : class
{
  void HasData(EntityTypeBuilder<T> entity);
}

