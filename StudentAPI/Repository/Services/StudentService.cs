using Microsoft.EntityFrameworkCore;
using StudentAPI.Database;
using StudentAPI.Model;
using StudentAPI.Repository.Interface;

namespace StudentAPI.Repository.Services
{
    public class StudentService : IRepository<Student>
    {
        public class StudetnDTO
        {
            public string Name { get; set; }
        }
        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        // without DI 
        //private ApplicationDbContext _context = new ApplicationDbContext();

        public async Task<List<Student>> GetAll()
        {
            //return  _context.Students.Select(x => new Student
            //{
            //    Name = x.Name

            //}).ToList();
            var data = await _context.Students.ToListAsync();
            return data;
        }
        public async Task<Student> GetById(int id)
        {
            var data = _context.Students.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return new Student();
            }
            return data;
        }
        public async Task<bool> AddStudent(Student student)
        {
            bool isCreated = false;
            await _context.Students.AddAsync(student);
            if (await _context.SaveChangesAsync() > 0)
            {
                isCreated = true;
            }
            return isCreated;
        }
        public async Task<bool> UpdateStudent(Student student)
        {
            bool isUpdated = false;
            var existingData = await _context.Students.FindAsync(student.Id);
            if (existingData == null)
                return isUpdated;
            existingData.Name = student.Name;
            existingData.Email = student.Email;
            existingData.Age = student.Age;
            if (await _context.SaveChangesAsync() > 0)
            {

                isUpdated = true;
            }
            return isUpdated;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            bool isDeleted = false;
            var existingData = await _context.Students.FindAsync(id);
            if (existingData == null)
                return isDeleted;
            _context.Remove(existingData);
            if (await _context.SaveChangesAsync() > 0)
            {
                isDeleted = true;
            }
            return isDeleted;
        }
    }
}
