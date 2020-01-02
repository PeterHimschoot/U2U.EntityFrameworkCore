#nullable enable

namespace U2U.EntityFrameworkCore.Abstractions
{
  /// <summary>
  /// EntityBase serves as the base class for all entities.
  /// </summary>
  public class EntityBase
  {
    public EntityBase(int id)
      => Id = id;

    public int Id { get; }
  }
}
