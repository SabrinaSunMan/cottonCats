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
                // [功能名稱].List/Main.Get/Post. DBModel TO ViewModel (縮寫 D To V 或是 V To D即可)
                cfg.AddProfile<BackEntityProfile>();
                cfg.AddProfile<AspNetUsersViewModelProfile>();
                cfg.AddProfile<AspNetUsersProfile>();

                cfg.AddProfile<StaticHtmlProfile>(); //[靜態網站管理].List.Get.D To V
                cfg.AddProfile<StaticHtmlViewModelProfile>(); //[靜態網站管理].Main.Post.V To D

                cfg.AddProfile<ActitiesProfile>(); //[活動紀錄管理].List.Get.D To V
                cfg.AddProfile<ActitiesViewModelProfile>(); //[活動紀錄管理].Main.Post.V To D
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
    /// [靜態網站管理].List.Get.D To V
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class StaticHtmlProfile : Profile
    {
        public override string ProfileName => base.ProfileName;

        //這邊負責確認是否兩個欄位有不同名稱
        public StaticHtmlProfile()
        {
            //CreateMap<StaticHtmlDetailViewModel, StaticHtmlDBViewModel>()
            CreateMap<StaticHtmlDBViewModel, StaticHtmlDetailViewModel>()

                .ForMember(dest => dest.StaticID, opt => opt.MapFrom(src => src.StaticID))
                .ForMember(dest => dest.picInfo, opt => opt.MapFrom(src => src.picInfo))
                .ForMember(dest => dest.HtmlContext, opt => opt.MapFrom(src => src.HtmlContext))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))

                .ForMember(dest => dest.CreateUser, opt => opt.MapFrom(src => src.CreateUser))
                .ForMember(dest => dest.sort, opt => opt.MapFrom(src => src.sort))
                .ForMember(dest => dest.SubjectID, opt => opt.MapFrom(src => src.SubjectID))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.SubjectName))
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime))

                .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.UpdateUser))
                .ForMember(dest => dest.StaticHtmlActionType, opt => opt.Ignore())
                .ForMember(dest => dest.PicGroupID, opt => opt.MapFrom(src => src.PicGroupID));
        }
    }

    /// <summary>
    /// [靜態網站管理].Main.Post.V To D
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class StaticHtmlViewModelProfile : Profile
    {
        public override string ProfileName => base.ProfileName;

        public StaticHtmlViewModelProfile()
        {
            CreateMap<StaticHtmlDetailViewModel, StaticHtml>()

                .ForMember(dest => dest.StaticID, opt => opt.MapFrom(src => src.StaticID))
                .ForMember(dest => dest.HtmlContext, opt => opt.MapFrom(src => src.HtmlContext))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))

                .ForMember(dest => dest.CreateUser, opt => opt.MapFrom(src => src.CreateUser))
                .ForMember(dest => dest.sort, opt => opt.MapFrom(src => src.sort))
                .ForMember(dest => dest.SubjectID, opt => opt.MapFrom(src => src.SubjectID))
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime))

                .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.UpdateUser))
                .ForMember(dest => dest.PicGroupID, opt => opt.MapFrom(src => src.PicGroupID));
            ;
        }
    }

    /// <summary>
    /// [活動紀錄管理].List.Get.D To V
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class ActitiesProfile : Profile
    {
        public override string ProfileName => base.ProfileName;

        //這邊負責確認是否兩個欄位有不同名稱
        public ActitiesProfile()
        {
            CreateMap<ActitiesDBViewModel, ActitiesDetailViewModel>()

                .ForMember(dest => dest.ActivityID, opt => opt.MapFrom(src => src.ActivityID))
                .ForMember(dest => dest.Sdate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Edate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.TitleName, opt => opt.MapFrom(src => src.TitleName))
                .ForMember(dest => dest.HtmlContext, opt => opt.MapFrom(src => src.HtmlContext))

                .ForMember(dest => dest.PicGroupID, opt => opt.MapFrom(src => src.PicGroupID))
                .ForMember(dest => dest.picInfo, opt => opt.MapFrom(src => src.picInfo))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))
                .ForMember(dest => dest.CreateUser, opt => opt.MapFrom(src => src.CreateUser))

                .ForMember(dest => dest.sort, opt => opt.MapFrom(src => src.sort))
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime))
                .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.UpdateUser));
        }
    }

    /// <summary>
    /// [活動紀錄管理].Main.Post.V To D
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class ActitiesViewModelProfile : Profile
    {
        public override string ProfileName => base.ProfileName;

        //這邊負責確認是否兩個欄位有不同名稱
        public ActitiesViewModelProfile()
        {
            CreateMap<ActitiesDetailViewModel, Activity>()

                .ForMember(dest => dest.ActivityID, opt => opt.MapFrom(src => src.ActivityID))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Sdate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Edate))
                .ForMember(dest => dest.TitleName, opt => opt.MapFrom(src => src.TitleName))
                .ForMember(dest => dest.HtmlContext, opt => opt.MapFrom(src => src.HtmlContext))

                .ForMember(dest => dest.PicGroupID, opt => opt.MapFrom(src => src.PicGroupID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))
                .ForMember(dest => dest.CreateUser, opt => opt.MapFrom(src => src.CreateUser))

                .ForMember(dest => dest.sort, opt => opt.MapFrom(src => src.sort))
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime))
                .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.UpdateUser));
        }
    }
}