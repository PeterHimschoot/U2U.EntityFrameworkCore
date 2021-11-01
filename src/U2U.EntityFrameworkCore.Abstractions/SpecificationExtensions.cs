namespace U2U.EntityFrameworkCore;

public static class SpecificationExtensions
{
  public static ISpecification<T> AsCached<T, K>(this ISpecification<T> spec, TimeSpan duration, K key)
    where T : class//, IEntity
  => new CachedSpecification<T, K>(spec.Criteria, spec.Includes, duration, key);

  public static ISpecification<T> Including<T>(this ISpecification<T> spec, Expression<Func<T, object>> include)
    where T : class//, IEntity
    => spec.Include(include);

  public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
    where T : class//, IEntity
    => And<T>(left, right.Criteria);

  public static ISpecification<T> And<T>(this ISpecification<T> left, Expression<Func<T, bool>> rightExpression)
    where T : class//, IEntity
  {
    Expression<Func<T, bool>> leftExpression = left.Criteria;

    var visitor = new SwapVisitor(leftExpression.Parameters[0], rightExpression.Parameters[0]);
    BinaryExpression lazyAnd = Expression.AndAlso(visitor.Visit(leftExpression.Body)!, rightExpression.Body);
    var and = Expression.Lambda<Func<T, bool>>(lazyAnd, rightExpression.Parameters);
    return new Specification<T>(and);
  }

  public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
    where T : class//, IEntity
    => Or<T>(left, right.Criteria);


  public static ISpecification<T> Or<T>(this ISpecification<T> left, Expression<Func<T, bool>> rightExpression)
    where T : class//, IEntity
  {
    Expression<Func<T, bool>> leftExpression = left.Criteria;
    var visitor = new SwapVisitor(leftExpression.Parameters[0], rightExpression.Parameters[0]);
    BinaryExpression lazyOr = Expression.OrElse(visitor.Visit(leftExpression.Body)!, rightExpression.Body);
    var or = Expression.Lambda<Func<T, bool>>(lazyOr, rightExpression.Parameters);
    return new Specification<T>(or);
  }

  public static ISpecification<T> Not<T>(this ISpecification<T> left)
    where T : class//, IEntity
  {
    Expression<Func<T, bool>> leftExpression = left.Criteria;
    UnaryExpression notExpression = Expression.Not(leftExpression.Body);
    var not = Expression.Lambda<Func<T, bool>>(notExpression, leftExpression.Parameters);
    return new Specification<T>(not);
  }

  private class SwapVisitor : ExpressionVisitor
  {
    private readonly Expression from, to;

    public SwapVisitor(Expression from, Expression to)
    {
      this.from = from;
      this.to = to;
    }

    public override Expression? Visit(Expression? node)
      => node == this.from ? this.to : base.Visit(node);
  }
}
