using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace BackMeow.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            //Mapper.Initialize();
        }
    }

    public class BackEntityProfile : Profile
    {
        public override string ProfileName => base.ProfileName;

        protected BackEntityProfile()
        {
           // CreateMap<>
        }
    }
}