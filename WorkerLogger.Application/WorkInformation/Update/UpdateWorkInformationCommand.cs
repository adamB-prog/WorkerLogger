using MediatR;

namespace WorkerLogger.Application.WorkInformation.Update;

public record UpdateWorkInformationCommand(Guid Id, string Title, string? Description, TimeSpan TimeSpent) : IRequest;
