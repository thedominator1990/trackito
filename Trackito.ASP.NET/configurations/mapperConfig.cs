using AutoMapper;
using Trackito.ASP.NET.Data;
using Trackito.ASP.NET.models.Aday;
using Trackito.ASP.NET.models.AlimentDays;
using Trackito.ASP.NET.models.Aliments;
using Trackito.ASP.NET.models.Exercise;
using Trackito.ASP.NET.models.Sets;
using Trackito.ASP.NET.models.Training;
using Trackito.ASP.NET.models.User;

namespace Trackito.ASP.NET.configurations
{
    public class mapperConfig : Profile
    {
        public mapperConfig()
        {
            //User
            CreateMap<UserCreateDTO, User>().ReverseMap();
            CreateMap<UserUpdateDTO, User>().ReverseMap();
            CreateMap<UserGetDTO, User>().ReverseMap();

            //ADAY
            CreateMap<AdayCreateDTO, Aday>().ReverseMap();
            CreateMap<AdayUpdateDTO, Aday>().ReverseMap();
            CreateMap<AdayGetOneDTO, Aday>().ReverseMap();

            //Aliment
            CreateMap<AlimentCreateDTO, Aliment>().ReverseMap();
            CreateMap<AlimentUpdateDTO, Aliment>().ReverseMap();
            CreateMap<AlimentGetDTO, Aliment>().ReverseMap();

            //Exercise
            CreateMap<ExerciseCreateDTO, Exercise>().ReverseMap();
            CreateMap<ExerciseUpdateDTO, Exercise>().ReverseMap();
            CreateMap<ExerciseGetDTO, Exercise>().ReverseMap();

            //AlimnentDay
            CreateMap<AlimentDayCreateDTO, AlimentDay>().ReverseMap();
            CreateMap<AlimentDayUpdateDTO, AlimentDay>().ReverseMap();
            CreateMap<AlimentDayGetDTO, AlimentDay>().ReverseMap();

            //Set
            CreateMap<SetCreateDTO, Set>().ReverseMap();
            CreateMap<SetUpdateDTO, Set>().ReverseMap();
            CreateMap<SetGetDTO, Set>().ReverseMap();

            //Training
            CreateMap<TrainingCreateDTO, training>().ReverseMap();
            CreateMap<TrainingUpdateDTO, training>().ReverseMap();
            CreateMap<TrainingGetDTO, training>().ReverseMap();

        }
    }
}
