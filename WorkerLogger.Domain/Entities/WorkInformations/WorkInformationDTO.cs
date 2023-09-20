namespace WorkerLogger.Domain.WorkInformations;

public record WorkInformationDTO(Guid Id, string Title, string? Description, TimeSpan TimeSpent, DateTime CreationTime);
