using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerLogger.Domain.Entities.ApplicationUsers
{
    public interface IUserRepository
    {
        void RegisterUser(ApplicationUser applicationUser);
        Task<ApplicationUser> GetApplicationUserById(string id);
    }
}
