using AutoMapper;
using BackMeow.Models.ViewModel;
using StoreDB.Model.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackMeow.AutoMapper
{
    /// <summary>
    /// 初始化 Mapping_AspNetUsers
    /// </summary>
    public class AspNetUsersInitialize
    {
        /// <summary>
        /// Mappers the AspNetUsers And AspNetUsersDetailViewModel.
        /// </summary>
        /// <param name="SourceViewModel">The source view model.</param>
        /// <returns></returns>
        public AspNetUsersDetailViewModel MapperAspNetUsersDetailViewModel(AspNetUsers SourceViewModel)
        {
            Mapper.Initialize(x => x.AddProfile<AspNetUsersProfile>());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AspNetUsersProfile>();
            });
            config.AssertConfigurationIsValid();//←證驗應對
            var mapper = config.CreateMapper();
            return mapper.Map<AspNetUsersDetailViewModel>(SourceViewModel);
        }
    }
}