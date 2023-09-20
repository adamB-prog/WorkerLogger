using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerLogger.Domain.Entities.WorkInformations;

namespace WorkerLogger.Infrastructure.Configurations
{
    internal class WorkInformationConfiguration : IEntityTypeConfiguration<WorkInformation>
    {
        public void Configure(EntityTypeBuilder<WorkInformation> builder)
        {
            //Megadjuk a táblák közötti kapcsolatokat és a tulajdonságokat.
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);

            builder.Property(x => x.TimeSpent).IsRequired();

            builder.HasOne(x => x.Owner)
                .WithMany(u => u.WorkInformations)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
