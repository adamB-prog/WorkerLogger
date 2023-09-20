namespace WorkerLogger.Domain.Primitives;

public class Entity
{
    public Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}

