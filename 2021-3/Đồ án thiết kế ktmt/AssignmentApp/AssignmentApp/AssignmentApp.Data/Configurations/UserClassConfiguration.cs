using AssignmentApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssignmentApp.Data.Configurations;

public class UserClassConfiguration : IEntityTypeConfiguration<UserClass>
{
    public void Configure(EntityTypeBuilder<UserClass> builder)
    {
        builder.ToTable("UserClasses");
        builder.HasKey(x => new { x.UserId, x.ClassId });
        // User quan he nhieu- nhieu voi class
        builder.HasOne(x => x.User).WithMany(x => x.UserClasses).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Class).WithMany(x => x.UserClasses).HasForeignKey(x => x.ClassId);
    }
}