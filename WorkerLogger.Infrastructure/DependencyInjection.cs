using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkerLogger.Application.Data;
using WorkerLogger.Domain.Entities.WorkInformations;
using WorkerLogger.Infrastructure.Repositories;

namespace WorkerLogger.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<WorkerLoggerDbContext>(options => 
            options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Programming\\WorkerLogger\\WorkerLogger\\WorkerLogger.Infrastructure\\WorkerLoggerDatabase.mdf;Integrated Security=True",
            b => b.MigrationsAssembly("WorkerLogger.Infrastructure"))
        );
        services.AddScoped<IWorkLoggerDbContext>(sp => sp.GetRequiredService<WorkerLoggerDbContext>());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<WorkerLoggerDbContext>());

        services.AddScoped<IWorkInformationRepository>(sp => sp.GetRequiredService<WorkInformationRepository>());

        services.AddScoped<IWorkInformationRepository, WorkInformationRepository>();
        

        
        return services;
    }
}