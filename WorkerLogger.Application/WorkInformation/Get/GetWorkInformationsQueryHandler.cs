using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkerLogger.Domain.Entities.ApplicationUsers;
using WorkerLogger.Domain.Entities.WorkInformations;
using WorkerLogger.Domain.WorkInformations;

namespace WorkerLogger.Application.WorkInformation.Get
{
    public class GetWorkInformationsQueryHandler : IRequestHandler<GetWorkInformationsQuery, IEnumerable<WorkInformationDTO>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IWorkInformationRepository workInformationRepository;

        public GetWorkInformationsQueryHandler(UserManager<ApplicationUser> userManager, IWorkInformationRepository workInformationRepository)
        {
            this.userManager = userManager;
            this.workInformationRepository = workInformationRepository;
        }
        /// <summary>
        /// Lekéri az adott felhasználó összes bejegyzését
        /// </summary>
        /// <param name="request">A felhasználó azonosítója</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IEnumerable<WorkInformationDTO>> Handle(GetWorkInformationsQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NullReferenceException("User not exists");
            }

            var workInformations = workInformationRepository
                .GetAllWorkInformations()
                .Where(w => w.OwnerId == request.UserId)
                .Select(x => new WorkInformationDTO(x.Id, x.Title, x.Description, x.TimeSpent, x.CreationTime))
                .ToList();


            return workInformations;
        }
    }
}
