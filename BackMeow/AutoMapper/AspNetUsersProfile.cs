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
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName)); 

        }
    }
}