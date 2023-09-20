using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkerLogger.Application.Data;
using WorkerLogger.Domain.Entities.ApplicationUsers;
using WorkerLogger.Domain.Entities.WorkInformations;

namespace WorkerLogger.Infrastructure
{
    public class WorkerLoggerDbContext : IdentityDbContext<ApplicationUser>, IWorkLoggerDbContext, IUnitOfWork
    {
        public DbSet<WorkInformation> WorkInformations { get; set; }


        public WorkerLoggerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkerLoggerDbContext).Assembly);
        }
    }
}
