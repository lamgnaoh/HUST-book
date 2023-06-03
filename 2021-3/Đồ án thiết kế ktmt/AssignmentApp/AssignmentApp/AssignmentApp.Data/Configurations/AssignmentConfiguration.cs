using AssignmentApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssignmentApp.Data.Configurations;

public class AssignmentConfiguration:IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.ToTable("Assignments");
        builder.HasKey(x => x.AssignmentId);
        builder.Property(x => x.Title).IsUnicode(true).HasMaxLength(50);
        builder.Property(x => x.Content).IsUnicode(true).HasMaxLength(500);
        builder.Property(x => x.CreateAt).IsRequired(true);
        builder.Property(x => x.DueTo).IsRequired(true);
    
        // quan he 1 - nhieu voi Class
        builder.HasOne(x => x.Class).WithMany(x => x.Assignments).HasForeignKey(x=> x.ClassId);
        // //quan he 1 - nhieu voi user tao assignment 
        // builder.HasOne(x => x.User).WithMany(x => x.Assignments).HasForeignKey(x => x.UserId);
    }
}