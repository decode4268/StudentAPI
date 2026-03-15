using StudentAPI.Database;
using StudentAPI.Model;
using StudentAPI.Repository.Interface;
using StudentAPI.Repository.Services;

namespace StudentAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public IRepository<Student> students { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            students = new StudentService(_context);
        }
        public async Task<int> Save()
        {
          return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
