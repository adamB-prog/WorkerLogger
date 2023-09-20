using Microsoft.EntityFrameworkCore;
using WorkerLogger.Domain.Entities.ApplicationUsers;
using WorkerLogger.Domain.Entities.WorkInformations;

namespace WorkerLogger.Application.Data
{
    public interface IWorkLoggerDbContext
    {
        DbSet<Domain.Entities.WorkInformations.WorkInformation> WorkInformations { get; set; }
        DbSet<ApplicationUser> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
