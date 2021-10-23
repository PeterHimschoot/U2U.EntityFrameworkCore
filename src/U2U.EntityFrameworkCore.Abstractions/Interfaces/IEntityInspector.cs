namespace U2U.EntityFrameworkCore.Abstractions.Interfaces;

public interface IEntityInspector
{
  void Inspect(InspectorContext context);
}

public interface IEntityInspector<T> : IEntityInspector { }

public interface IEntityInspectors
{
  IEnumerable<IEntityInspector> All { get; }
}

