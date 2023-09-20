using Microsoft.AspNetCore.Identity;
using WorkerLogger.Domain.Entities.WorkInformations;

namespace WorkerLogger.Domain.Entities.ApplicationUsers;

public class ApplicationUser : IdentityUser
{
    public ICollection<WorkInformation>? WorkInformations { get; set; }

    public ApplicationUser()
    {
        WorkInformations = new List<WorkInformation>();
    }
}

