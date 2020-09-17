namespace U2U.EntityFrameworkCore.Abstractions
{
  /// <summary>
  /// Interface to mark EntityBase as AggregateRoot
  /// </summary>
  public interface IAggregateRoot
  {

  }

  public interface IAggregateRoot<T1> : IAggregateRoot
  {

  }

  // public class ShoppingBasket : IAggregateRoot<Customer, GameInBasket>

  public interface IAggregateRoot<T1, T2>
  {

  }
}
