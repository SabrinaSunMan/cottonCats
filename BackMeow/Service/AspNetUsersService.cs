using BackMeow.Models.ViewModel;
using PagedList;
using StoreDB.Model.Partials;
using StoreDB.Enum;
using StoreDB.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BackMeow.Service
{
    //取得使用者資料 
    public class AspNetUsersService
    {
        Repository<AspNetUsers> AspNetUsersRepository = new Repository<AspNetUsers>();
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
                AspNetUsersRepository.GetAll().OrderByDescending(s => s.UserName).ToList();
            return GetAspNetUsers;
            //  return book_info.GetAll().OrderByDescending(s => s.DateTimes); 
        }

        /// <summary>
        /// Gets the ASP net users by identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public AspNetUsers GetAspNetUsersById(string guid)
        {
            return AspNetUsersRepository.GetSingle(guid);
        }
        
    }
}