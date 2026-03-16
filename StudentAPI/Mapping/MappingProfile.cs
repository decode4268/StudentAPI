using AutoMapper;
using StudentAPI.DTO;
using StudentAPI.Model;

namespace StudentAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();
        }
    }
}
