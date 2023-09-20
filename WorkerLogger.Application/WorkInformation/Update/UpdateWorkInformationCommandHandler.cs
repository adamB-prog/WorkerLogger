using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerLogger.Application.Data;
using WorkerLogger.Domain.Entities.WorkInformations;

namespace WorkerLogger.Application.WorkInformation.Update
{
    public class UpdateWorkInformationCommandHandler : IRequestHandler<UpdateWorkInformationCommand>
    {
        private readonly IWorkInformationRepository workInformationRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateWorkInformationCommandHandler(IWorkInformationRepository workInformationRepository, IUnitOfWork unitOfWork)
        {
            this.workInformationRepository = workInformationRepository;
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Frissíti az adott bejegyzést
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(UpdateWorkInformationCommand request, CancellationToken cancellationToken)
        {
            var workInformation = new Domain.Entities.WorkInformations.WorkInformation(request.Id, request.Title, request.Description,request.TimeSpent);

            workInformationRepository.UpdateWorkInformation(workInformation);
            await unitOfWork.SaveChangesAsync(cancellationToken);


        }
    }
}
