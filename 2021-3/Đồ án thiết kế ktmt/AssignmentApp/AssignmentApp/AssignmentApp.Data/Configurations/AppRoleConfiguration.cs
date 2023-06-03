using AssignmentApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssignmentApp.Data.Configurations;

public class AppRoleConfiguration: IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable("AppRoles");
        builder.HasKey(x => x.RoleId);
        builder.Property(x => x.Name).IsRequired(true);
        // builder.HasMany(x => x.Users).WithOne(x => x.AppRole).HasForeignKey(x=> x.RoleId);
    }
}