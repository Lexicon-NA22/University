using AutoMapper;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities;

namespace University.Web.Automapper
{
    public class UniversityMappings : Profile
    {
        public UniversityMappings()
        {

            CreateMap<Student, StudentCreateViewModel>().ReverseMap();
            CreateMap<Student, StudentIndexViewModel>();
            CreateMap<Student, StudentDetailsViewModel>()
                .ForMember(
                       dest => dest.Attending,
                       from => from.MapFrom(s => s.Enrollments.Count));

            CreateMap<Student, StudentEditViewModel>().ReverseMap();

        }
    }
}
