namespace U2U.EntityFrameworkCore.Abstractions.Interfaces;

public interface ISpecificationFactory
{
  bool ForType(Type t);
}

public interface ISpecificationFactory<T> : ISpecificationFactory
{
}
