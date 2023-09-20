using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerLogger.Domain.Entities.WorkInformations
{
    public interface IWorkInformationRepository
    {
        void AddWorkInformation(WorkInformation workInformation);
        IEnumerable<WorkInformation> GetAllWorkInformations();

        WorkInformation GetWorkInformationById(Guid id);

        void DeleteWorkInformation(Guid id);

        void UpdateWorkInformation(WorkInformation workInformation);
    }
}
