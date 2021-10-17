namespace U2U.EntityFrameworkCore;

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

    Visit(a);

    if (this.candidates.Count > 0)
    {
      Stop();
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
    => CheckEqual(expression.NodeType, candidate.NodeType)
    && CheckEqual(expression.Type, candidate.Type);

  private void Stop()
    => this.areEqual = false;

  // Change the type of candidate to match original
  private T? MakeCandidateMatch<T>([NotNull] T original) where T : Expression
    => (T?)this.candidate;

  public override Expression? Visit(Expression? expression)
  {
    if (expression == null || !AreEqual)
    {
      return expression;
    }

    this.candidate = PeekCandidate();
    if (!CheckNotNull(this.candidate) || !CheckAreOfSameType(this.candidate!, expression))
    {
      return expression;
    }

    PopCandidate();

    return base.Visit(expression);
  }

  protected override Expression? VisitConstant(ConstantExpression constant)
  {
    ConstantExpression? candidate = MakeCandidateMatch(constant);
    CheckEqual(constant.Value, candidate?.Value);
    return base.VisitConstant(constant);
  }

  protected override Expression? VisitMember(MemberExpression member)
  {
    MemberExpression? candidate = MakeCandidateMatch(member);
    CheckEqual(member.Member, candidate?.Member);
    return base.VisitMember(member);
  }

  protected override Expression? VisitMethodCall(MethodCallExpression methodCall)
  {
    MethodCallExpression? candidate = MakeCandidateMatch(methodCall);
    CheckEqual(methodCall.Method, candidate?.Method);
    return base.VisitMethodCall(methodCall);
  }

  protected override Expression? VisitParameter(ParameterExpression parameter)
  {
    ParameterExpression? candidate = MakeCandidateMatch(parameter);
    CheckEqual(parameter.Name, candidate?.Name);
    return base.VisitParameter(parameter);
  }

  protected override Expression VisitTypeBinary(TypeBinaryExpression type)
  {
    TypeBinaryExpression? candidate = MakeCandidateMatch(type);
    CheckEqual(type.TypeOperand, candidate!.TypeOperand);
    return base.VisitTypeBinary(type);
  }

  protected override Expression VisitBinary(BinaryExpression binary)
  {
    BinaryExpression? candidate = MakeCandidateMatch(binary);
    CheckEqual(binary.Method, candidate!.Method);
    CheckEqual(binary.IsLifted, candidate!.IsLifted);
    CheckEqual(binary.IsLiftedToNull, candidate!.IsLiftedToNull);
    return base.VisitBinary(binary);
  }

  protected override Expression VisitUnary(UnaryExpression unary)
  {
    UnaryExpression? candidate = MakeCandidateMatch(unary);
    CheckEqual(unary.Method, candidate!.Method);
    CheckEqual(unary.IsLifted, candidate.IsLifted);
    CheckEqual(unary.IsLiftedToNull, candidate.IsLiftedToNull);
    return base.VisitUnary(unary);
  }

  protected override Expression VisitNew(NewExpression nex)
  {
    NewExpression? candidate = MakeCandidateMatch(nex);
    CheckEqual(nex.Constructor, candidate!.Constructor);
    CompareList(nex.Members, candidate.Members);
    return base.VisitNew(nex);
  }

  private void CompareList<T>(ReadOnlyCollection<T> collection, ReadOnlyCollection<T> candidates)
    => CompareList(collection, candidates, (item, candidate) => EqualityComparer<T>.Default.Equals(item, candidate));

  private void CompareList<T>(ReadOnlyCollection<T> collection, ReadOnlyCollection<T> candidates, Func<T, T, bool> comparer)
  {
    if (!CheckAreOfSameSize(collection, candidates))
    {
      return;
    }

    for (int i = 0; i < collection.Count; i++)
    {
      if (!comparer(collection[i], candidates[i]))
      {
        Stop();
        return;
      }
    }
  }

  private bool CheckAreOfSameSize<T>(ReadOnlyCollection<T> collection, ReadOnlyCollection<T> candidate)
    => CheckEqual(collection.Count, candidate.Count);

  private bool CheckNotNull<T>(T? t) where T : class
  {
    if (t == null)
    {
      Stop();
      return false;
    }

    return true;
  }

  private bool CheckEqual<T>(T t, T candidate)
  {
    if (!EqualityComparer<T>.Default.Equals(t, candidate))
    {
      Stop();
      return false;
    }

    return true;
  }
}

