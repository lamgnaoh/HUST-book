using AssignmentApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = System.IO.File;

namespace AssignmentApp.Data.Configurations;

public class FileConfiguration:IEntityTypeConfiguration<Data.Entities.File>
{
    
    public void Configure(EntityTypeBuilder<Entities.File> builder)
    {
        
            builder.ToTable("Files");
            builder.HasKey(x=> x.FileId);
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(80);
            builder.Property(x => x.Path).HasColumnType("nvarchar").HasMaxLength(200);
            builder.HasOne(x=>x.StudentAssignment).WithMany(x=>x.SubmitFiles);
    }
}