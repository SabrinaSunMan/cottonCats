using AutoMapper;
using BackMeow.Models.ViewModel;
using StoreDB.Model.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackMeow.AutoMapper
{
    public class AspNetUsersProfile : Profile
    { 
        public override string ProfileName => base.ProfileName;

        //這邊負責確認是否兩個欄位有不同名稱
        public AspNetUsersProfile()
        {
            CreateMap<AspNetUsers, AspNetUsersDetailViewModel>()
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))

                //.ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src=>src.PasswordHash)); 

        }
    }
}