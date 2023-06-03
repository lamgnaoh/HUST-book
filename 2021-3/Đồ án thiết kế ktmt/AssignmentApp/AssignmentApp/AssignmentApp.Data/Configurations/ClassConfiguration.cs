using AssignmentApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssignmentApp.Data.Configurations;

public class ClassConfiguration:IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Classes");
        builder.HasKey(x => x.ClassId);
        builder.Property(x => x.Name).IsUnicode(true).IsRequired(true).HasMaxLength(50);
        // builder.HasOne(x => x.UserCreate).WithMany(x => x.Classes).HasForeignKey(x => x.UserCreateId);
    }
}