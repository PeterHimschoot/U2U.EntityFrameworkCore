//namespace U2U.EntityFrameworkCore;

//public abstract class EntityInspector<T> : IEntityInspector<T>
//  where T : class
//{
//  public abstract void Inspect(InspectorContext context);
//}

// YAGNI

//public abstract class BinaryEntityInspector<T> : EntityInspector<T>
//  where T : class
//{
//  protected readonly IEntityInspector left;
//  protected readonly IEntityInspector right;

//  public BinaryEntityInspector(IEntityInspector<T> left, IEntityInspector<T> right)
//  {
//    this.left = left;
//    this.right = right;
//  }
//}

//public class AndEntityInspector<T> : BinaryEntityInspector<T>
//  where T : class
//{
//  public AndEntityInspector(IEntityInspector<T> left, IEntityInspector<T> right)
//    : base(left, right) { }

//  public override bool Inspect(EntityEntry entry, DateTime timestamp)
//    => this.left.Inspect(entry, timestamp) && this.right.Inspect(entry, timestamp);
//}

//public class OrEntityInspector<T> : BinaryEntityInspector<T>
//  where T : class
//{
//  public OrEntityInspector(IEntityInspector left, IEntityInspector right)
//    : base(left, right) { }

//  public override bool Inspect(EntityEntry entry, DateTime timestamp)
//    => this.left.Inspect(entry, timestamp) || this.right.Inspect(entry, timestamp);
//}

//public class NotEntityInspector<T> : EntityInspector<T>
//  where T : class
//{
//  private readonly IEntityInspector inner;

//  public NotEntityInspector(IEntityInspector inner)
//    => this.inner = inner;

//  public override bool Inspect(EntityEntry entry, DateTime timestamp)
//    => !(this.inner.Inspect(entry, timestamp));
//}

//public static class IEntityInspectorExtensions
//{
//  public static EntityInspector<T> And<T>(this EntityInspector<T> left, EntityInspector<T> right)
//   where T : class
//   => new AndEntityInspector<T>(left, right);

//  public static EntityInspector<T> Or<T>(this EntityInspector<T> left, EntityInspector<T> right)
//    where T : class
//  => new OrEntityInspector<T>(left, right);

//  public static EntityInspector<T> Not<T>(this EntityInspector<T> inner)
//   where T : class
//   => new NotEntityInspector<T>(inner);
//}

