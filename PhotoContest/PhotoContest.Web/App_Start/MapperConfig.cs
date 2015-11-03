using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PhotoContest.Models;
using PhotoContest.Web.Models.BindingModel;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.App_Start
{
    public static class MapperConfig
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<Contest, ContestViewModelHomePage>();

            Mapper.CreateMap<Contest, ContestViewModelActiveInactivePage>()
                .ForMember(model => model.OwnerUsername, config => config.MapFrom(contest => contest.Owner.UserName));

            Mapper.CreateMap<Contest, ContestUserProfileViewModel>();

            Mapper.CreateMap<Contest, ContestViewModel>();
            Mapper.CreateMap<Contest, ContestViewModel>().
                ForMember(model => model.OwnerName, config => config.MapFrom(contest => contest.Owner.UserName)).
                ForMember(model => model.DeadlineStrategy, config => config.MapFrom(contest => contest.DeadlineStrategy)).
                ForMember(model => model.ParticipationStrategy, config => config.MapFrom(contest => contest.ParticipationStrategy)).
                ForMember(model => model.RewardStrategy, config => config.MapFrom(contest => contest.RewardStrategy)).
                ForMember(model => model.VotingStrategy, config => config.MapFrom(contest => contest.VotingStrategy));

            Mapper.CreateMap<Contest, ContestFullViewModel>()
                .ForMember(model => model.OwnerName, config => config.MapFrom(contest => contest.Owner.UserName)).
                ForMember(model => model.DeadlineStrategy, config => config.MapFrom(contest => contest.DeadlineStrategy)).
                ForMember(model => model.ParticipationStrategy, config => config.MapFrom(contest => contest.ParticipationStrategy)).
                ForMember(model => model.RewardStrategy, config => config.MapFrom(contest => contest.RewardStrategy)).
                ForMember(model => model.VotingStrategy, config => config.MapFrom(contest => contest.VotingStrategy))
                .ForMember(model => model.ContestPictures, config => config.MapFrom(contest => contest.ContestPictures));

            Mapper.CreateMap<Contest, EditContestBindingModel>().ReverseMap();

            Mapper.CreateMap<ContestPicture, ContestPictureUserProfileViewModel>()
                .ForMember(model => model.ConstestTitle, config => config.MapFrom(contestPicture => contestPicture.Contest.Title));

            Mapper.CreateMap<ContestPicture, ListPictureViewModel>()
                .ForMember(model => model.OwnerUserName, config => config.MapFrom(a => a.Owner.UserName));

        }
    }
}