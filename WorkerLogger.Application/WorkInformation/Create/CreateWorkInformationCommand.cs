using MediatR;

namespace WorkerLogger.Application.WorkInformation.Create;

public record CreateWorkInformationCommand(string UserId, string Title, string? Description, TimeSpan TimeSpent) : IRequest;
