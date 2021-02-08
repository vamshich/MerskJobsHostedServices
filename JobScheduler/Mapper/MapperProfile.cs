using AutoMapper;
using JobScheduler.Models;
using JobScheduler.Database.Entity;
using System.Collections.Generic;

namespace JobScheduler.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<JobsManager, JobsManagerDTO>()
                .ForMember(des => des.TimeTakenToProcess, opts => opts.MapFrom(source => (source.CompletedTime.HasValue)
                     ? (source.CompletedTime.Value - source.ReceivedTime.Value).ToString(@"h\:mm\:ss") : "NA"));
        }
    }
}
