using MediatR;
using WorkerLogger.Domain.WorkInformations;

namespace WorkerLogger.Application.WorkInformation.Get;

public record GetWorkInformationsQuery(string UserId) : IRequest<IEnumerable<WorkInformationDTO>>;

