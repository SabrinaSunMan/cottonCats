using BackMeow.Models.ViewModel;
using PagedList;
using StoreDB.Model.Partials;
using StoreDB.Enum;
using StoreDB.Repositories;
using System.Collections.Generic;
using System.Linq;
using StoreDB.Interface;
using System;
using AutoMapper;

namespace BackMeow.Service
{
    //取得使用者資料 
    public class AspNetUsersService
    {
        private readonly IRepository<AspNetUsers> _AspNetUsersRep;
        private readonly IRepository<NLog_Error> _NLog_ErrorRep;
        private readonly IUnitOfWork _unitOfWork;

        public AspNetUsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _AspNetUsersRep = new Repository<AspNetUsers>(unitOfWork);
            _NLog_ErrorRep = new Repository<NLog_Error>(unitOfWork);
        }

        //Repository<AspNetUsers> AspNetUsersRepository = new Repository<AspNetUsers>();
        private readonly int pageSize = (int)BackPageListSize.commonSize;

        /// <summary>
        /// Gets the system roles ListView model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <param name="nowpage">The nowpage.</param>
        /// <returns></returns>
        public SystemRolesListViewModel GetSystemRolesListViewModel(SystemRolesListHeaderViewModel selectModel, int nowpage)
        {
            SystemRolesListViewModel returnSystemRolesListViewModel = new SystemRolesListViewModel();
            int currentPage = nowpage < 1 ? 1 : nowpage;
            returnSystemRolesListViewModel.Header = selectModel; /*表頭*/
            returnSystemRolesListViewModel.Content_List = GetAllSystemRolesListViewModel(selectModel).ToPagedList(currentPage, pageSize);/*內容*/
            return returnSystemRolesListViewModel;
        }

        /// <summary>
        /// Gets all system roles ListView model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <returns></returns>
        private IEnumerable<SystemRolesListContentViewModel> GetAllSystemRolesListViewModel(SystemRolesListHeaderViewModel selectModel)
        {
            IEnumerable<SystemRolesListContentViewModel> result = GetAllAspNetUsers().Where(s => (!string.IsNullOrEmpty(selectModel.UserName) ?
            s.UserName.Contains(selectModel.UserName) : s.UserName == s.UserName) 
            && (!string.IsNullOrWhiteSpace(selectModel.Email) ?
            s.Email.Contains(selectModel.Email) : s.Email == s.Email) 

                ).Select(List => new SystemRolesListContentViewModel()
                {
                    Email = List.Email,
                    UserName = List.UserName,
                    PhoneNumber = List.PhoneNumber,
                    LockoutEnabled = List.LockoutEnabled
                }).ToList();

            return result;
        }

        /// <summary>
        /// Gets all ASP net users.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AspNetUsers> GetAllAspNetUsers()
        {
            IEnumerable<AspNetUsers> GetAspNetUsers =
            _AspNetUsersRep.GetAll().OrderByDescending(s => s.UserName).ToList();
            return GetAspNetUsers;
        }

        public AspNetUsersDetailViewModel ReturnAspNetUsersDetail(Actions ActionType,string guid)
        {
            AspNetUsersDetailViewModel DetailViewModel = new AspNetUsersDetailViewModel();
            //Mapper.C
            //DetailViewModel.
            AspNetUsers AspNetUsersViewModel = GetAspNetUsersById(guid);
            return DetailViewModel;
        }

        /// <summary>
        /// Gets the ASP net users by identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        private AspNetUsers GetAspNetUsersById(string guid)
        {
            return _AspNetUsersRep.GetSingle(s=>s.Id == guid);
        }

        public void Add(AspNetUsers aspuser)
        {
            //aspuser.Id = Guid.NewGuid();
            aspuser.CreateTime = DateTime.Now;
            _AspNetUsersRep.Create(aspuser);
            //var isMember = _NLog_Error.Query(d => d.Account == order.Email).Any();
            //if (isMember == false)
            //{
            //    var newMember = new Members()
            //    {
            //        Id = Guid.NewGuid(),
            //        Account = order.Email,
            //        Password = Guid.NewGuid().ToString()
            //    };
            //    _membersRep.Create(newMember);
            //}
        }

    }
}