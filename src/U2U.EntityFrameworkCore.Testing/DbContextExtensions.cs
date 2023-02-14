namespace U2U.EntityFrameworkCore.Testing;

public class FakeDbContextBuilder<TDB>
where TDB : DbContext
{
  private readonly Mock<TDB> moq = new();

  public TDB Build()
  => moq.Object;

  public void Replace<E>(Expression<Func<TDB, DbSet<E>>> exp, DbSet<E> e)
    where E : class => moq.Setup(exp).Returns(e);
}

public static class DbContextExtensions
{
  public static FakeDbContextBuilder<TDB> WithTable<TDB, E>(
    this TDB dbContext,
    Expression<Func<TDB, DbSet<E>>> exp,
    IQueryable<E> data)
    where TDB : DbContext
    where E : class
  {
    FakeDbContextBuilder<TDB> builder = new();
    return builder.WithTable(exp, data);
  }

  public static FakeDbContextBuilder<TDB> WithTable<TDB, E>(
    this FakeDbContextBuilder<TDB> builder,
    Expression<Func<TDB, DbSet<E>>> exp,
    IQueryable<E> data)
    where TDB : DbContext
    where E : class
  {
    Mock<DbSet<E>> mockSet = new();
    _ = mockSet.As<IQueryable<E>>()
      .Setup(m => m.Provider)
      .Returns(data.Provider);
    _ = mockSet.As<IQueryable<E>>()
      .Setup(m => m.Expression)
      .Returns(data.Expression);
    _ = mockSet.As<IQueryable<E>>()
      .Setup(m => m.ElementType)
      .Returns(data.ElementType);
    _ = mockSet.As<IQueryable<E>>()
      .Setup(m => m.GetEnumerator())
      .Returns(data.GetEnumerator);

    builder.Replace(exp, mockSet.Object);
    return builder;
  }
}

