using AutoMapper;
using Service.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Model;

namespace Service.Mapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<User, DtoUserprofile>().ReverseMap();
            //CreateMap<UserActivation, User>().ReverseMap();
            //CreateMap<UserMusicTrack, MusicTrack>().ReverseMap();
        }
    }
}
