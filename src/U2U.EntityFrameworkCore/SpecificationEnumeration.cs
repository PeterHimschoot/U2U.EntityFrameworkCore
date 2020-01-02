#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace U2U.EntityFrameworkCore
{
  internal class ExpressionEnumeration : ExpressionVisitor, IEnumerable<Expression>
  {
    private List<Expression> expressions = new List<Expression>();

    public ExpressionEnumeration(Expression expression)
      => Visit(expression);

    public override Expression? Visit(Expression? expression)
    {
      if (expression == null)
      {
        return expression;
      }
      expressions.Add(expression);
      return base.Visit(expression);
    }

    public IEnumerator<Expression> GetEnumerator()
      => expressions.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
      => (this as IEnumerable<Expression>).GetEnumerator();
  }
}
