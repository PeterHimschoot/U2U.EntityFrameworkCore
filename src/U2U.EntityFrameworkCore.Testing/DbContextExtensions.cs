using NSubstitute;

namespace U2U.EntityFrameworkCore.Testing;

public class FakeDbContextBuilder<TDB>
where TDB : DbContext
{
  private readonly TDB _sub = Substitute.For<TDB>();

  public TDB Build()
  => _sub;

  //public void Replace<E>(Expression<Func<TDB, DbSet<E>>> exp, DbSet<E> e)
  //  where E : class => moq.Setup(exp).Returns(e);
  public void Replace<E>(Expression<Func<TDB, DbSet<E>>> exp, DbSet<E> e)
  where E : class
  {
    _sub.When(x => exp.Compile().Invoke(x))
        .Do(call => call[0]= e);
  }
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

  //public static FakeDbContextBuilder<TDB> WithTable<TDB, E>(
  //  this FakeDbContextBuilder<TDB> builder,
  //  Expression<Func<TDB, DbSet<E>>> exp,
  //  IQueryable<E> data)
  //  where TDB : DbContext
  //  where E : class
  //{
  //  DbSet<E> mockSet = Substitute.For<DbSet<E>>().;
  //  _ = mockSet.   As<IQueryable<E>>()
  //    .Provider
  //    .Returns(data.Provider);
  //  _ = mockSet.As<IQueryable<E>>()
  //    .Setup(m => m.Expression)
  //    .Returns(data.Expression);
  //  _ = mockSet.As<IQueryable<E>>()
  //    .Setup(m => m.ElementType)
  //    .Returns(data.ElementType);
  //  _ = mockSet.As<IQueryable<E>>()
  //    .Setup(m => m.GetEnumerator())
  //    .Returns(data.GetEnumerator);

  //  builder.Replace(exp, mockSet.Object);
  //  return builder;
  //}

  public static FakeDbContextBuilder<TDB> WithTable<TDB, E>(
    this FakeDbContextBuilder<TDB> builder,
    Expression<Func<TDB, DbSet<E>>> exp,
    IQueryable<E> data)
    where TDB : DbContext
    where E : class
  {
    DbSet<E> mockSet = Substitute.For<DbSet<E>, IQueryable<E>>();
    ((IQueryable<E>)mockSet).Provider.Returns(data.Provider);
    ((IQueryable<E>)mockSet).Expression.Returns(data.Expression);
    ((IQueryable<E>)mockSet).ElementType.Returns(data.ElementType);
    ((IQueryable<E>)mockSet).GetEnumerator().Returns(data.GetEnumerator());
    builder.Replace(exp, mockSet);
    return builder;
  }
}

