using AutoMapper;
using StoreDB.Model.Partials;
using BackMeow.Models.ViewModel;
using StoreDB.Model.ViewModel.BackcottonCats;

namespace BackMeow.App_Start
{
    public static class AutoMapperConfig
    {
        //public static void Configure()
        //{
        //    //Mapper.Initialize();
        //    Mapper.Initialize(x => x.AddProfile<BackEntityProfile>());
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile<BackEntityProfile>();
        //    });
        //} 
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BackEntityProfile>();
                cfg.AddProfile<AspNetUsersViewModelProfile>();
                cfg.AddProfile<AspNetUsersProfile>();
                //cfg.AddProfile<StaticHtmlProfile>(); //靜態網站管理 _List頁面
                /*etc...*/
            });
            config.AssertConfigurationIsValid();//←證驗應對
            return config;
        }
    }

    public class BackEntityProfile : Profile
    {
        public override string ProfileName => base.ProfileName;

        public BackEntityProfile()
        {
            CreateMap<MenuTreeRootStratumViewModel, MenuSideContentViewModel>()
                .ForMember(dest => dest.ActionName, opt => opt.MapFrom(src => src.ActionName)).
                ForMember(dest => dest.ControllerName, opt => opt.MapFrom(src => src.ControllerName)).
                //ForMember(dest => dest.nowPicker, opt => opt.Ignore()).
                //ForMember(dest => dest.tree, opt => opt.Ignore()).
                ForMember(dest => dest.MenuName, opt => opt.Ignore()).
                ForMember(dest => dest.MenuOrder, opt => opt.Ignore()).
                ForMember(dest => dest.TRootID, opt => opt.MapFrom(src => src.TRootID)).
                ForMember(dest => dest.TRootName, opt => opt.MapFrom(src => src.TRootName)).
                ForMember(dest => dest.TRootOrder, opt => opt.MapFrom(src => src.TRootOrder)).
                ForMember(dest => dest.UrlIcon, opt => opt.MapFrom(src => src.UrlIcon)).
                IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }

    public class AspNetUsersViewModelProfile : Profile
    {
        public override string ProfileName => base.ProfileName;

        //這邊負責確認是否兩個欄位有不同名稱
        public AspNetUsersViewModelProfile()
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
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Old_Password, opt => opt.Ignore());
        }
    }

    public class AspNetUsersProfile : Profile
    {
        public override string ProfileName => base.ProfileName;

        //這邊負責確認是否兩個欄位有不同名稱
        public AspNetUsersProfile()
        {
            CreateMap<AspNetUsersDetailViewModel, AspNetUsers>()

                //.ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                .ForMember(dest => dest.AspNetRoles, opt => opt.Ignore())
                .ForMember(dest => dest.AspNetUserClaims, opt => opt.Ignore())
                .ForMember(dest => dest.AspNetUserLogins, opt => opt.Ignore())
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))

                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.GroupID, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())

                .ForMember(dest => dest.LockoutEndDateUtc, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())

                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }

    /// <summary>
    /// 靜態網站管理 _List頁面
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    //public class StaticHtmlProfile : Profile
    //{
    //    public override string ProfileName => base.ProfileName;

    //    //這邊負責確認是否兩個欄位有不同名稱
    //    public StaticHtmlProfile()
    //    {
    //        CreateMap<StaticHtmlListContentViewModel, StaticHtmlDBViewModel>()

    //            .ForMember(dest => dest.StaticID, opt => opt.MapFrom(src => src.CreateTime))
    //            .ForMember(dest => dest.PictureName, opt => opt.MapFrom(src => src.PictureName))
    //            .ForMember(dest => dest.HtmlContext, opt => opt.MapFrom(src => src.HtmlContext))
    //            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
    //            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))

    //            .ForMember(dest => dest.CreateUser, opt => opt.Ignore())
    //            .ForMember(dest => dest.FileExtension, opt => opt.Ignore())
    //            .ForMember(dest => dest.PicID, opt => opt.Ignore())
    //            .ForMember(dest => dest.PictureUrl, opt => opt.Ignore())
    //            .ForMember(dest => dest.sort, opt => opt.Ignore())

    //            .ForMember(dest => dest.SubjectID, opt => opt.Ignore())
    //            .ForMember(dest => dest.SubjectName, opt => opt.Ignore())
    //            .ForMember(dest => dest.UpdateTime, opt => opt.Ignore())
    //            .ForMember(dest => dest.UpdateUser, opt => opt.Ignore());
    //    }
    //}
}