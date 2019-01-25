using BackMeow.App_Start;
using BackMeow.Models.ViewModel;
using PagedList;
using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Repositories;
using StoreDB.Enum;
using System.Collections.Generic;
using System.Linq;
using StoreDB.Helper;

namespace BackMeow.Service
{
    /// <summary>
    /// 後台 使用者資料 相關
    /// </summary>
    public class AspNetUsersService
    {
        //private readonly IRepository<AspNetUsers> _AspNetUsersRep;
        //private readonly IRepository<NLog_Error> _NLog_ErrorRep;
        //private readonly IRepository<Addresses> _Addresses;
        private readonly AspNetUsersRepository _AspNetUsersRep;

        private MenuSideListService _menuSideListService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly int pageSize = (int)BackPageListSize.commonSize;

        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public AspNetUsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_NLog_ErrorRep = new Repository<NLog_Error>(unitOfWork);
            //_Addresses = new Repository<Addresses>(unitOfWork);
            _AspNetUsersRep = new AspNetUsersRepository(unitOfWork);
            _menuSideListService = new MenuSideListService(unitOfWork);
        }

        /// <summary>
        /// Gets the system roles ListView model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <param name="nowpage">The nowpage.</param>
        /// <returns></returns>
        public SystemRolesViewModel GetSystemRolesListViewModel(SystemRolesListHeaderViewModel selectModel, int nowpage = 1)
        {
            SystemRolesViewModel returnSystemRolesListViewModel = new SystemRolesViewModel();
            returnSystemRolesListViewModel.Header = selectModel; /*表頭*/
            returnSystemRolesListViewModel.page = nowpage;
            IEnumerable<SystemRolesListContentViewModel> GetAllSystemRolesListViewModelResult = GetAllSystemRolesListViewModel(selectModel);
            int currentPage = (nowpage < 1) && GetAllSystemRolesListViewModelResult.Count() >= 1 ? 1 : nowpage;
            returnSystemRolesListViewModel.Content_List = GetAllSystemRolesListViewModelResult.ToPagedList(currentPage, pageSize);/*內容*/
            return returnSystemRolesListViewModel;
        }

        /// <summary>
        /// Gets all system roles ListView model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <returns></returns>
        private IEnumerable<SystemRolesListContentViewModel> GetAllSystemRolesListViewModel(SystemRolesListHeaderViewModel selectModel)
        {
            IEnumerable<SystemRolesListContentViewModel> result =
                GetAllAspNetUsers().Where(s => (!string.IsNullOrEmpty(selectModel.UserName) ?
                                    s.UserName.ToUpper().Contains(selectModel.UserName.ToUpper()) : true)
                                        && (!string.IsNullOrWhiteSpace(selectModel.Email) ?
                                    s.Email.ToUpper().Contains(selectModel.Email.ToUpper()) : true)
                                        && s.Status == true).Select(List => new SystemRolesListContentViewModel()
                                        {
                                            Id = List.Id,
                                            Email = List.Email,
                                            UserName = List.UserName,
                                            PhoneNumber = List.PhoneNumber,
                                            LockoutEnabled = List.LockoutEnabled
                                        }).OrderByDescending(s => s.UserName).ToList();
            return result;
        }

        public AspNetUsersDetailViewModel ReturnAspNetUsersDetail(DataAction ActionType, string guid)
        {
            AspNetUsersDetailViewModel DetailViewModel = new AspNetUsersDetailViewModel();
            AspNetUsers AspNetUsersViewModel = GetAspNetUsersById(guid);
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            DetailViewModel = mapper.Map<AspNetUsersDetailViewModel>(AspNetUsersViewModel);
            //DetailViewModel = _aspnetMapping.MapperAspNetUsersDetailViewModel(AspNetUsersViewModel);
            return DetailViewModel;
        }

        /// <summary>
        /// 取得所有管理者.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AspNetUsers> GetAllAspNetUsers()
        {
            IEnumerable<AspNetUsers> GetAspNetUsers =
            _AspNetUsersRep.GetAll().OrderByDescending(s => s.UserName).ToList();
            return GetAspNetUsers;
        }

        /// <summary>
        /// 藉由參數取得管理者資訊.
        /// </summary>
        /// <returns></returns>
        public AspNetUsers GetAspNetUserBySelectPramters()
        {
            AspNetUsers GetAspNetUsers = _AspNetUsersRep.Query(s => s.UserName.Equals(UserName) || s.Email.Equals(UserEmail)).FirstOrDefault();
            return GetAspNetUsers;
        }

        /// <summary>
        /// Gets the ASP net users by identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public AspNetUsers GetAspNetUsersById(string guid)
        {
            return _AspNetUsersRep.GetSingle(s => s.Id == guid);
        }

        public void AspNetUsersDetailViewModelUpdate(AspNetUsersDetailViewModel viewModel, string CreateUser)
        {
            AspNetUsers AspNetUsers = new AspNetUsers();
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            AspNetUsers = mapper.Map<AspNetUsers>(viewModel);
            _AspNetUsersRep.AspNetUserUpdate(AspNetUsers, CreateUser);
        }

        /// <summary>
        /// 藉由使用者ID 開通權限. 日後可更改權限群組，現階段先以全部開通為基準
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public void CreateUserMenuTree(string guid)
        {
            _menuSideListService.CreateMenuTree(guid);
        }

        /// <summary>
        /// Deletes the User.
        /// </summary>
        /// <returns></returns>
        public string DeleteUser(string guid, string UpdateUser)
        {
            try
            {
                AspNetUsers aspUsers = _AspNetUsersRep.GetSingle(s => s.Id.Equals(guid));
                aspUsers.Status = false;
                _AspNetUsersRep.AspNetUserUpdate(aspUsers, UpdateUser);
                return EnumHelper.GetEnumDescription(DataAction.DeleteScuess);
            }
            catch
            {
                return EnumHelper.GetEnumDescription(DataAction.DeleteFail);
            }
        }

        public void Save()
        {
            //_AspNetUsersRep.Commit();
            _unitOfWork.Save();
        }
    }
}