namespace U2U.EntityFrameworkCore;

internal class ExpressionEnumeration : ExpressionVisitor, IEnumerable<Expression>
{
  private readonly List<Expression> expressions = new();

  public ExpressionEnumeration(Expression expression)
    => Visit(expression);

  public override Expression? Visit(Expression? expression)
  {
    if (expression == null)
    {
      return expression;
    }
    this.expressions.Add(expression);
    return base.Visit(expression);
  }

  public IEnumerator<Expression> GetEnumerator()
    => this.expressions.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator()
    => (this as IEnumerable<Expression>).GetEnumerator();
}

