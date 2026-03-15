using StudentAPI.Model;

namespace StudentAPI.Repository.Interface
{
    public interface IRepository<T> where T : class
    {                                                         
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> AddStudent(T entity);
        Task<bool> UpdateStudent(T entity);
        Task<bool> DeleteStudent(int id);
    }
}
