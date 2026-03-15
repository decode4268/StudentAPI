using StudentAPI.Model;
using StudentAPI.Repository.Interface;

namespace StudentAPI.Repository
{
    public interface IUnitOfWork: IDisposable
    {                          
        IRepository<Student> students { get; set; }
        Task<int> Save();
    }
}
