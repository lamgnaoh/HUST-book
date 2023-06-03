using System.Net;
using AssignmentApp.Data.Configurations;
using AssignmentApp.Data.Entities;
using AssignmentApp.Data.Extension;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using File = System.IO.File;

namespace AssignmentApp.Data.EF;

public class AssignmentAppDbContext :DbContext
{
    public AssignmentAppDbContext(DbContextOptions options) : base(options)
    {
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    //         .AddJsonFile("appsettings.json").Build();
    //     var connectionString = configuration.GetConnectionString("AssignmentAppDatabase");
    //     optionsBuilder.UseSqlServer(connectionString);
    //     
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // add config cua cac entity o day
        modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
        modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
        modelBuilder.ApplyConfiguration(new ClassConfiguration());
        modelBuilder.ApplyConfiguration(new StudentAssignmentConfiguration());
        modelBuilder.ApplyConfiguration(new UserClassConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new FileConfiguration());
        // modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("AppUserClaims");
        // modelBuilder.Entity<IdentityUserRole<int>>().ToTable("AppUserRoles");
        // modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("AppUserLogins");
        // modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("AppRoleClaims");
        // modelBuilder.Entity<IdentityUserToken<int>>().ToTable("AppUserTokens");
        
        //Data seeding 
        modelBuilder.Seed();
        // base.OnModelCreating(modelBuilder);
    }

    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<StudentAssignment> StudentAssignments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserClass> UserClasses { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    public DbSet<Entities.File> Files { get; set; }

}