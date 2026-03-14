using StudentAPI.Model;

namespace StudentAPI.Repository.Interface
{
    public interface IRepository
    {                                                         
        Task<List<Student>> GetAll();
        Task<Student> GetById(int id);
        Task<bool> AddStudent(Student student);
        Task<bool> UpdateStudent(Student student);
        Task<bool> DeleteStudent(int id);
    }
}
