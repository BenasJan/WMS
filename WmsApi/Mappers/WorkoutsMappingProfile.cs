using AutoMapper;
using WmsApi.Contracts;

namespace WmsApi.Mappers;

public class WorkoutsMappingProfile : Profile
{
    public WorkoutsMappingProfile()
    {
        CreateMap<Exercise, Database.Models.Exercise>()
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => new TimeSpan(src.Duration)))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.WorkoutId, opt => opt.Ignore())
            .ForMember(dest => dest.Workout, opt => opt.Ignore())
            ;
        
        CreateMap<Workout, Database.Models.Workout>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.WorkoutDate, opt => opt.Ignore())
            ;

        CreateMap<Database.Models.Workout, WorkoutSummary>()
            .ForMember(dest => dest.TotalDuration, opt => opt.MapFrom(src => new TimeSpan(src.Exercises.Sum(e => e.Duration.Ticks))))
            .ForMember(dest => dest.TotalDuration, opt => opt.MapFrom(src => src.Exercises.Sum(e => e.Reps)))
            .ForMember(dest => dest.TotalDuration, opt => opt.MapFrom(src => src.Exercises.Sum(e => e.Sets)))
            ;

        CreateMap<Database.Models.Workout, CalendarWorkout>()
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src))
            ;
    }
}