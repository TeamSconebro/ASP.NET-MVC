﻿using System;
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
            Mapper.CreateMap<Contest, ContestViewModel>().
                ForMember(model => model.OwnerName, config => config.MapFrom(contest => contest.Owner.UserName)).
                ForMember(model => model.DeadlineStrategy, config => config.MapFrom(contest => contest.DeadlineStrategy)).
                ForMember(model => model.ParticipationStrategy, config => config.MapFrom(contest => contest.ParticipationStrategy)).
                ForMember(model => model.RewardStrategy, config => config.MapFrom(contest => contest.RewardStrategy)).
                ForMember(model => model.VotingStrategy, config => config.MapFrom(contest => contest.VotingStrategy));

            //Mapper.CreateMap<Post, PostConciseViewModel>()
            //    .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));
            //Mapper.CreateMap<Post, PostFullViewModel>()
            //    .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));
        }
    }
}