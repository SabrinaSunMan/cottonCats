using AutoMapper;
using StoreDB.Model.Partials;
using BackMeow.Models.ViewModel;
using StoreDB.Model.ViewModel;

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
                /*etc...*/
            });

            return config;
        } 
    }

    public  class BackEntityProfile : Profile
    { 
        public override string ProfileName => base.ProfileName;

        public BackEntityProfile()
        {
            //CreateMap<AspNetUsers, AspNetUsersDetailViewModel>();
            //CreateMap<AspNetUsers, AspNetUsersDetailViewModel>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));

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
}