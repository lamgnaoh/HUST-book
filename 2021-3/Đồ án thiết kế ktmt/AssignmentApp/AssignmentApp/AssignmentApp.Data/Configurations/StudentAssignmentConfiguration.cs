using AssignmentApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssignmentApp.Data.Configurations;

public class StudentAssignmentConfiguration:IEntityTypeConfiguration<StudentAssignment>
{
    public void Configure(EntityTypeBuilder<StudentAssignment> builder)
    {
        builder.ToTable("StudentAssignments");
        builder.HasKey(x => new { x.AssignmentId, x.StudentId });
        builder.Property(x => x.Grade).IsRequired(false);
        builder.Property(x => x.Feedback).IsUnicode(true).HasMaxLength(500).IsRequired(false);
        builder.Property(x => x.SubmittedAt).IsRequired(false);
        //quan he 1 - nhieu voi assignment
        builder.HasOne(x => x.Assignment).WithMany(x => x.StudentAssignments).HasForeignKey(x => x.AssignmentId);
        
        //quan he 1 - nhieu voi User student 
        builder.HasOne(x => x.User).WithMany(x => x.StudentAssignments).HasForeignKey(x => x.StudentId);
    }
}