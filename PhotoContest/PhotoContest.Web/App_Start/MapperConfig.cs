using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PhotoContest.Models;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.App_Start
{
    public static class MapperConfig
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<Contest, ContestViewModel>();
            //Mapper.CreateMap<Post, PostConciseViewModel>()
            //    .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));
            //Mapper.CreateMap<Post, PostFullViewModel>()
            //    .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));
        }
    }
}