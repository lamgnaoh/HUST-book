using AssignmentApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using File = AssignmentApp.Data.Entities.File;

namespace AssignmentApp.Data.Extension;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppRole>().HasData(
            new AppRole() { RoleId = 1, Name = "admin" },
            new AppRole() { RoleId = 2, Name = "teacher" },
            new AppRole() { RoleId = 3, Name = "student" }
        );
        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1, Username = "Luong Hoang Lam 20183780", Password = "12345678", PhoneNumber = "0123123xxx",
                Email = "lam.lh183780@sis.hust.edu.vn", MSSV = "20183780", FullName = "Luong Hoang Lam" 
            },
            new User()
            {
                Id = 2, Username = "Dang Bao Lam 20183779", Password = "12345678", PhoneNumber = "0456456xxx",
                Email = "lam.db183779@sis.hust.edu.vn", MSSV = "20183779", FullName = "Dang Bao Lam"
            },
            new User()
            {
                Id = 3, Username = "Nguyen Dinh Thuan", Password = "12345678", PhoneNumber = "0789789xxx",
                Email = "thuan.nguyendinh@hust.edu.vn", FullName = "Nguyen Dinh Thuan" 
            },
            new User()
            {
                Id = 4, Username = "admin", Password = "admin", PhoneNumber = "0456789xxx",
                Email = "admin@hust.edu.vn", FullName = "admin"
            }
        );
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole() { UserId = 1, RoleId = 3 },
            new UserRole() { UserId = 2, RoleId = 3 },
            new UserRole() { UserId = 3, RoleId = 2 },
            new UserRole() { UserId = 3, RoleId = 1 },
            new UserRole() { UserId = 4, RoleId = 1 }
        );
        modelBuilder.Entity<Class>().HasData(
            new Class() { ClassId = 1, Name = "project 20213" , CreateAt = DateTime.Parse("08/06/2022 23:30:00") },
            new Class() { ClassId = 2, Name = "Lap trinh .NET Core" , CreateAt = DateTime.Parse("11/05/2022 15:30:00")}
        );
        modelBuilder.Entity<UserClass>().HasData(
            new UserClass() { UserId = 1, ClassId = 1 },
            new UserClass() { UserId = 2, ClassId = 1 },
            new UserClass() { UserId = 3, ClassId = 1 },
            new UserClass() { UserId = 1, ClassId = 2 },
            new UserClass() { UserId = 3, ClassId = 2 }
        );

        modelBuilder.Entity<Assignment>().HasData(
            new Assignment()
            {
                AssignmentId = 1, ClassId = 1, CreateAt = DateTime.Parse("06/09/2022 23:30:00"), DueTo = DateTime.Parse("08/09/2022 23:30:00"),
                Title = "Báo cáo tuần 1", Content = "Nộp báo cáo tuần 1 "
            },
            new Assignment()
            {
                AssignmentId = 2, ClassId = 2, CreateAt = DateTime.Parse("08/09/2022 23:30:00"), DueTo = DateTime.Parse("09/09/2022 23:30:00"),
                Title = ".NET WEB API", Content = "Lập trình .NET WEB API"
            }
        );

        modelBuilder.Entity<StudentAssignment>().HasData(
            new StudentAssignment()
            {
                AssignmentId = 1, StudentId = 1, Submitted = true, Grade = 10, Feedback = "Tốt",
                SubmittedAt = DateTime.Parse("07/09/2022 23:30:00")
            },
            new StudentAssignment()
            {
                AssignmentId = 1, StudentId = 2, Submitted = false
            },
            new StudentAssignment()
            {
                AssignmentId = 2, StudentId = 1, Submitted = true,SubmittedAt = DateTime.Parse("07/09/2022 23:30:00")
            }
        );
    }
}