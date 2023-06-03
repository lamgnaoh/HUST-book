using File = AssignmentApp.Data.Entities.File;

namespace AssignmentApp.API.Repository.Files;

public interface IFileRepository
{
    Task<List<File>> GetFileFromStudentAssignment(int assignmentId, int studentId);
}