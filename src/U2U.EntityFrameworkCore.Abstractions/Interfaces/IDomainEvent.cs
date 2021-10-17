namespace U2U.EntityFrameworkCore.Abstractions.Interfaces;

public interface IDomainEvent { }

public interface IDomainEvent<T> : IDomainEvent where T : EntityBase { }
