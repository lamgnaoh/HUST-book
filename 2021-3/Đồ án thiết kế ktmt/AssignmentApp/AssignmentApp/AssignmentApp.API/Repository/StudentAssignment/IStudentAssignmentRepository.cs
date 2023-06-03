namespace AssignmentApp.API.Repository.StudentAssignment;

public interface IStudentAssignmentRepository
{
    Task<List<Data.Entities.StudentAssignment>> GetAllAssignedAssignment(int StudentId);
    Task<List<Data.Entities.StudentAssignment>> GetAllStudentAssignmentForAssignment(int AssignmentId);
    Task<Data.Entities.StudentAssignment> GetStudentAssignment(int AssignmentId , int studentId);

    Task<Data.Entities.StudentAssignment> GradeAssignment(Data.Entities.StudentAssignment markedStudentAssignment,int AssignmentId, int StudentId);
}