#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace U2U.EntityFrameworkCore
{
  internal class ExpressionComparison : ExpressionVisitor
  {
    private bool areEqual = true;

    private readonly Queue<Expression> candidates;
    private Expression? candidate;

    public bool AreEqual => this.areEqual;

    public ExpressionComparison(Expression a, Expression b)
    {
      this.candidates = new Queue<Expression>(new ExpressionEnumeration(b));
      this.candidate = null;

      this.Visit(a);

      if (this.candidates.Count > 0)
      {
        this.Stop();
      }
    }

    private Expression? PeekCandidate()
    {
      if (this.candidates.Count == 0)
      {
        return null;
      }

      return this.candidates.Peek();
    }

    private Expression PopCandidate()
      => this.candidates.Dequeue();

    private bool CheckAreOfSameType(Expression candidate, Expression expression) 
      => this.CheckEqual(expression.NodeType, candidate.NodeType)
      && this.CheckEqual(expression.Type, candidate.Type);

    private void Stop()
      => this.areEqual = false;

    // Change the type of candidate to match original
    private T? MakeCandidateMatch<T>([NotNull] T original) where T : Expression
      => (T?)this.candidate;

    public override Expression? Visit(Expression? expression)
    {
      if (expression == null || !this.AreEqual)
      {
        return expression;
      }

      this.candidate = this.PeekCandidate();
      if( !this.CheckNotNull(this.candidate) || !this.CheckAreOfSameType(this.candidate!, expression))
      {
        return expression;
      }

      this.PopCandidate();

      return base.Visit(expression);
    }

    protected override Expression? VisitConstant(ConstantExpression constant)
    {
      ConstantExpression? candidate = this.MakeCandidateMatch(constant);
      this.CheckEqual(constant.Value, candidate?.Value);
      return base.VisitConstant(constant);
    }

    protected override Expression? VisitMember(MemberExpression member)
    {
      MemberExpression? candidate = this.MakeCandidateMatch(member);
      this.CheckEqual(member.Member, candidate?.Member);
      return base.VisitMember(member);
    }

    protected override Expression? VisitMethodCall(MethodCallExpression methodCall)
    {
      MethodCallExpression? candidate = this.MakeCandidateMatch(methodCall);
      this.CheckEqual(methodCall.Method, candidate?.Method);
      return base.VisitMethodCall(methodCall);
    }

    protected override Expression? VisitParameter(ParameterExpression parameter)
    {
      ParameterExpression? candidate = this.MakeCandidateMatch(parameter);
      this.CheckEqual(parameter.Name, candidate?.Name);
      return base.VisitParameter(parameter);
    }

    protected override Expression VisitTypeBinary(TypeBinaryExpression type)
    {
      TypeBinaryExpression? candidate = this.MakeCandidateMatch(type);
      this.CheckEqual(type.TypeOperand, candidate!.TypeOperand);
      return base.VisitTypeBinary(type);
    }

    protected override Expression VisitBinary(BinaryExpression binary)
    {
      BinaryExpression? candidate = this.MakeCandidateMatch(binary);
      this.CheckEqual(binary.Method, candidate!.Method);
      this.CheckEqual(binary.IsLifted, candidate!.IsLifted);
      this.CheckEqual(binary.IsLiftedToNull, candidate!.IsLiftedToNull);
      return base.VisitBinary(binary);
    }

    protected override Expression VisitUnary(UnaryExpression unary)
    {
      UnaryExpression? candidate = this.MakeCandidateMatch(unary);
      this.CheckEqual(unary.Method, candidate!.Method);
      this.CheckEqual(unary.IsLifted, candidate.IsLifted);
      this.CheckEqual(unary.IsLiftedToNull, candidate.IsLiftedToNull);
      return base.VisitUnary(unary);
    }

    protected override Expression VisitNew(NewExpression nex)
    {
      NewExpression? candidate = this.MakeCandidateMatch(nex);
      this.CheckEqual(nex.Constructor, candidate!.Constructor);
      this.CompareList(nex.Members, candidate.Members);
      return base.VisitNew(nex);
    }

    private void CompareList<T>(ReadOnlyCollection<T> collection, ReadOnlyCollection<T> candidates)
      => this.CompareList(collection, candidates, (item, candidate) => EqualityComparer<T>.Default.Equals(item, candidate));

    private void CompareList<T>(ReadOnlyCollection<T> collection, ReadOnlyCollection<T> candidates, Func<T, T, bool> comparer)
    {
      if (!this.CheckAreOfSameSize(collection, candidates))
      {
        return;
      }

      for (int i = 0; i < collection.Count; i++)
      {
        if (!comparer(collection[i], candidates[i]))
        {
          this.Stop();
          return;
        }
      }
    }

    private bool CheckAreOfSameSize<T>(ReadOnlyCollection<T> collection, ReadOnlyCollection<T> candidate)
      => this.CheckEqual(collection.Count, candidate.Count);

    private bool CheckNotNull<T>(T? t) where T : class
    {
      if (t == null)
      {
        this.Stop();
        return false;
      }

      return true;
    }

    private bool CheckEqual<T>(T t, T candidate)
    {
      if (!EqualityComparer<T>.Default.Equals(t, candidate))
      {
        this.Stop();
        return false;
      }

      return true;
    }
  }
}
