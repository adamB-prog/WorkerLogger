using System.ComponentModel.DataAnnotations.Schema;
using WorkerLogger.Domain.Entities.ApplicationUsers;
using WorkerLogger.Domain.Primitives;

namespace WorkerLogger.Domain.Entities.WorkInformations
{
    public class WorkInformation : Entity
    {
        public WorkInformation(Guid id, string title, string? description, TimeSpan timeSpent) : base(id)
        {
            Title = title;
            Description = description;
            TimeSpent = timeSpent;
            CreationTime = DateTime.Now;
        }

        public string Title { get; set; }

        public string? Description { get; set; }

        public TimeSpan TimeSpent { get; set; }

        public DateTime CreationTime { get; set; }

        public string? OwnerId { get; set; }

        [NotMapped]
        public ApplicationUser? Owner {  get; set; }
    }
}
