using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerLogger.Application.Data;
using WorkerLogger.Domain.Entities.ApplicationUsers;
using WorkerLogger.Domain.Entities.WorkInformations;

namespace WorkerLogger.Application.WorkInformation.Create
{
    public class CreateWorkInformationCommandHandler : IRequestHandler<CreateWorkInformationCommand>
    {
        private readonly IWorkInformationRepository workInformationRepository;

        private readonly IUnitOfWork unitOfWork;

        private readonly UserManager<ApplicationUser> userManager;

        public CreateWorkInformationCommandHandler(IWorkInformationRepository workInformationRepository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.workInformationRepository = workInformationRepository;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        /// <summary>
        /// Elkészít egy új elemet és az adott felhasználóhoz rendeli.
        /// </summary>
        /// <param name="request">Adatok ami szükséges a létrehozáshoz</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(CreateWorkInformationCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);


            var newWorkInformation = new Domain.Entities.WorkInformations.WorkInformation(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.TimeSpent
                );

            
            
            user.WorkInformations.Add(newWorkInformation);
            workInformationRepository.AddWorkInformation(newWorkInformation);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
