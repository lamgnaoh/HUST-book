using AssignmentApp.Data.Entities;

namespace AssignmentApp.API.Repository.Assignments;

public interface IAssignmentRepository
{
    Task<Assignment> CreateAssignment(Assignment createAssignment);
    Task<Assignment> UpdateAssignment(Assignment updateAssignment , int id);
    Task<Assignment> DeleteAssignment(int assignmentId);
    Task<List<Assignment>> GetAllByClass(int classId);
    Task<Assignment> GetAssignment(int id);
    Task<List<Assignment>> GetAll();
    Task<Data.Entities.StudentAssignment> SubmitAssignment(int AssignmentId , int studentId ,List<IFormFile> files);
}

