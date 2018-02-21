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
using BackMeow.App_Start;
using BackMeow.Models;
using StoreDB.Model;

namespace BackMeow.Service
{
    /// <summary>
    /// 後台 使用者資料 相關
    /// </summary>
    public class AspNetUsersService
    {
        private readonly IRepository<AspNetUsers> _AspNetUsersRep;
        private readonly IRepository<NLog_Error> _NLog_ErrorRep;
        private readonly IRepository<Addresses> _Addresses;

        public string UserName { get; set; }
        public string UserEmail { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        
        public AspNetUsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _AspNetUsersRep = new Repository<AspNetUsers>(unitOfWork);
            _NLog_ErrorRep = new Repository<NLog_Error>(unitOfWork);
            _Addresses = new Repository<Addresses>(unitOfWork);
        }

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
            IEnumerable<SystemRolesListContentViewModel> result = 
                GetAllAspNetUsers().Where(s => (!string.IsNullOrEmpty(selectModel.UserName) ?
            s.UserName.Contains(selectModel.UserName) : s.UserName == s.UserName)
            && (!string.IsNullOrWhiteSpace(selectModel.Email) ?
            s.Email.Contains(selectModel.Email) : s.Email == s.Email)

                ).Select(List => new SystemRolesListContentViewModel()
                {
                    Id = List.Id,
                    Email = List.Email,
                    UserName = List.UserName,
                    PhoneNumber = List.PhoneNumber,
                    LockoutEnabled = List.LockoutEnabled
                }).ToList();
            //IEnumerable<AspNetUsers> ListViewModel = GetAllAspNetUsers().Where(s => (!string.IsNullOrEmpty(selectModel.UserName) ?
            //s.UserName.Contains(selectModel.UserName) : s.UserName == s.UserName)
            //&& (!string.IsNullOrWhiteSpace(selectModel.Email) ?
            //s.Email.Contains(selectModel.Email) : s.Email == s.Email));
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<AspNetUsers, SystemRolesListContentViewModel>());

            //config.AssertConfigurationIsValid();//←證驗應對
            //var mapper = config.CreateMapper();
            //IEnumerable<SystemRolesListContentViewModel> result = 
            //    mapper.Map<SystemRolesListContentViewModel>(ListViewModel);
            return result;
        }
         
        public AspNetUsersDetailViewModel ReturnAspNetUsersDetail(Actions ActionType,string guid)
        {
            AspNetUsersDetailViewModel DetailViewModel = new AspNetUsersDetailViewModel();
            AspNetUsers AspNetUsersViewModel = GetAspNetUsersById(guid);  
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper(); 
            DetailViewModel  = mapper.Map<AspNetUsersDetailViewModel>(AspNetUsersViewModel);
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
            AspNetUsers GetAspNetUsers =
            _AspNetUsersRep.GetAll().Where(s => s.UserName == (string.IsNullOrEmpty(UserName) ? s.UserName : UserName)
            && s.Email == (string.IsNullOrEmpty(UserEmail) ? s.Email : UserEmail)).First();
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
         
        public void Add(AspNetUsers aspuser)
        { 
            //Students test = new Students();
            //test.studentName = "TESTStudent";
            //_Students.Create(test);


            //aspuser.Id = Guid.NewGuid().ToString().ToUpper();
            //aspuser.CreateTime = DateTime.Now;
            //_AspNetUsersRep.Create(aspuser);
            //////var isMember = _AspNetUsersRep.Query(s => s.Id == aspuser.Id).Any();
            //////if (isMember == false)
            //////{
            //////    var newMember = new AspNetUsers()
            //////    {
            //////        Id = Guid.NewGuid().ToString().ToUpper(),
            //////        Email = aspuser.Email,
            //////        UserName = "TEST",
            //////        CreateTime = DateTime.Now
            //////    };
            //////    _AspNetUsersRep.Create(newMember);
            //////} 

            #region TEST ADd
            //Addresses add = new Addresses(); 
            //add.AddressContent = "測試輸入";
            //_Addresses.Create(add);

            #endregion
        }

        //public ReturnMsg ReturnUpdateResult(AspNetUsersDetailViewModel viewModel)
        //{
        //    ReturnMsg Results = new ReturnMsg();
        //    //if (AspNetUsersDetailViewModelUpdate(viewModel))
        //    //{
        //    //    Results.enumMsg = BackReturnMsg.Scuess;
        //    //}
        //    //else Results.enumMsg = BackReturnMsg.Error;
        //    return Results;
        //}

        public void AspNetUsersDetailViewModelUpdate(AspNetUsersDetailViewModel viewModel)
        { 
            AspNetUsers AspNetUsers = new AspNetUsers();
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            AspNetUsers = mapper.Map<AspNetUsers>(viewModel);
            
            //StoreDBContext a = new StoreDBContext();
            //a.Entry(AspNetUsers).State = System.Data.Entity.EntityState.Modified;
            //AspNetUsers get =  a.AspNetUsers.Where(s=>s.Id==AspNetUsers.Id).FirstOrDefault();
            //get.UserName = AspNetUsers.UserName;
            ////get = AspNetUsers;
            //a.SaveChanges();
            Update(AspNetUsers);

            Addresses add = new Addresses();
            add.AddressContent = "測試輸入";
            _Addresses.Create(add);
        }

        private void Update(AspNetUsers aspuser)
        {
            aspuser.UpdateTime = DateTime.Now;
            _AspNetUsersRep.Update(aspuser); 
        }

        public void Save()
        {
            //_AspNetUsersRep.Commit();
            _unitOfWork.Save();
        }
    }
}