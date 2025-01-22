using NSubstitute;

namespace U2U.EntityFrameworkCore.Testing;

public class FakeDbContextBuilder<TDB>
where TDB : DbContext
{
  public TDB Build() => _sub;

  internal readonly TDB _sub = Substitute.For<TDB>();

  internal void Replace<E>(TDB sub, Func<TDB, DbSet<E>> exp, DbSet<E> e)
    where E : class => exp(sub).Returns(e);
}

public static class DbContextExtensions
{
  public static FakeDbContextBuilder<TDB> WithTable<TDB, E>(
    this TDB dbContext,
    Func<TDB, DbSet<E>> exp,
    IQueryable<E> data)
    where TDB : DbContext
    where E : class
  {
    FakeDbContextBuilder<TDB> builder = new();
    return builder.WithTable(exp, data);
  }

  public static FakeDbContextBuilder<TDB> WithTable<TDB, E>(
    this FakeDbContextBuilder<TDB> builder,
    Func<TDB, DbSet<E>> exp,
    IQueryable<E> data)
    where TDB : DbContext
    where E : class
  {
    DbSet<E> mockSet = Substitute.For<DbSet<E>, IQueryable<E>>();
    _ = ((IQueryable<E>)mockSet).Provider.Returns(data.Provider);
    _ = ((IQueryable<E>)mockSet).Expression.Returns(data.Expression);
    _ = ((IQueryable<E>)mockSet).ElementType.Returns(data.ElementType);
    _ = ((IQueryable<E>)mockSet).GetEnumerator().Returns(data.GetEnumerator());
    builder.Replace(builder._sub, exp, mockSet);
    return builder;
  }
}

