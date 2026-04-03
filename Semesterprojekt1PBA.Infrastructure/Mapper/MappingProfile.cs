using AutoMapper;
using Semesterprojekt1PBA.Application.Dto.School;
using Semesterprojekt1PBA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<School, GetDetailedSchoolResponse>();
        }
    }
}
