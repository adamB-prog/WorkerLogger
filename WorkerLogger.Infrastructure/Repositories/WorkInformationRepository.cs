using Microsoft.EntityFrameworkCore;
using WorkerLogger.Application.Data;
using WorkerLogger.Domain.Entities.WorkInformations;

namespace WorkerLogger.Infrastructure.Repositories;

public class WorkInformationRepository : IWorkInformationRepository
{
    private readonly IWorkLoggerDbContext workLoggerDbContext;

    public WorkInformationRepository(IWorkLoggerDbContext workLoggerDbContext)
    {
        this.workLoggerDbContext = workLoggerDbContext;
    }

    public void AddWorkInformation(WorkInformation workInformation)
    {
        workLoggerDbContext.WorkInformations.Add(workInformation);
    }

    public void DeleteWorkInformation(Guid id)
    {
        var needToBeDeleted = workLoggerDbContext.WorkInformations.Find(id);
        if (needToBeDeleted != null)
        {
            workLoggerDbContext.WorkInformations.Remove(needToBeDeleted);
        }
    }

    public IEnumerable<WorkInformation> GetAllWorkInformations()
    {
        var result = workLoggerDbContext.WorkInformations.ToList();
        return result;
    }

    public WorkInformation GetWorkInformationById(Guid id)
    {
        var result = workLoggerDbContext.WorkInformations.Find(id);

        return result;
    }

    public void UpdateWorkInformation(WorkInformation workInformation)
    {
        var result = GetWorkInformationById(workInformation.Id);
        
        if (result == null)
        {
            return;
        }

        result.Title = workInformation.Title;
        result.Description = workInformation.Description;
        result.TimeSpent = workInformation.TimeSpent;
    }
}
