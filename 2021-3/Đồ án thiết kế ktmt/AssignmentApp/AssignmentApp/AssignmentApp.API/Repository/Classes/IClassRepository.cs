using System.Net.Sockets;
using AssignmentApp.Data.Entities;
using AutoMapper.Configuration.Conventions;

namespace AssignmentApp.API.Repository.Classes;

public interface IClassRepository
{
    Task<List<Class>> GetAll();
    Task<Class> CreateClass(Class createClass);
    Task<Class> UpdateClass(Class updateClass , int classId);
    Task<Class> DeleteClass(int classId);
    Task<Class> GetClass(int classId);
    Task<List<Class>> GetALlAttended(int userId);

    Task<UserClass> AddUserToClass(int classId, int userId);
    Task<UserClass> RemoveUserToClass(int classId, int userId);
    Task<List<User>> GetAllUserInClass(int classId);
    Task<bool> IsUserInClass(int classId, int userId);
}