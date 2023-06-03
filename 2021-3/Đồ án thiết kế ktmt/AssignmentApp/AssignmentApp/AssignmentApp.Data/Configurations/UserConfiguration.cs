using AssignmentApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssignmentApp.Data.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Username).HasMaxLength(60).HasColumnType("varchar").IsRequired(true);
        builder.Property(x => x.Password).HasMaxLength(10).IsRequired(true);
        builder.Property(x => x.PhoneNumber).HasMaxLength(15).IsRequired(true).HasColumnType("varchar");
        builder.Property(x => x.Email).HasMaxLength(50).IsRequired(true).HasColumnType("varchar");
        builder.Property(x => x.MSSV).HasMaxLength(10).HasColumnType("varchar").IsRequired(false);
        builder.Property(x => x.FullName).HasMaxLength(50).IsRequired(true).HasColumnType("varchar");
    }
}