using BackMeow.Models.ViewModel;
using BackMeow.Service;
using StoreDB.Enum;
using StoreDB.Model.Partials;
using StoreDB.Repositories;
using System.Web.Mvc;

namespace BackMeow.Controllers
{
    public class SystemController : Controller
    {
        private readonly AspNetUsersService _UserService;
        //private AspNetUsersService _UserService = new AspNetUsersService();

        public SystemController()
        {
            var unitOfWork = new EFUnitOfWork();
            _UserService = new AspNetUsersService(unitOfWork); 
        }
        // GET: System 
        #region 後台使用者管理

        #region 取得所有後台使用者清單
        [HttpGet]
            public ActionResult SystemRolesList()
            {
                SystemRolesListViewModel ResultViewModel = _UserService.GetSystemRolesListViewModel(new SystemRolesListHeaderViewModel(), 1);
                return View(ResultViewModel);
            }

            [HttpPost]
            public ActionResult SystemRolesList(SystemRolesListViewModel SystemRolesListViewModel,int page = 1)
            {
                SystemRolesListViewModel ResultViewModel = _UserService.GetSystemRolesListViewModel(SystemRolesListViewModel.Header, page);
                return View(ResultViewModel);
            }
            #endregion
            #region 藉由ID取得後台使用者
            [HttpGet]
            public ActionResult SystemRolesMain(Actions ActionType,string guid)
            {
                //if(ActionType==Actions.Update)
                //{
                    
                //}else
                //{
                //    return View(new )   
                //}
                
                return View();
            }

            [HttpPost]
            public ActionResult SystemRolesMain(AspNetUsers AspNetUsersModel, Actions actions)
            {
            if (ModelState.IsValid)
            {
                if(actions== Actions.Create)
                {

                }
                else
                {

                }
                //_BookInfoService.Create(book);
            }
            return RedirectToAction("Bookkeeping");

            //AspNetUsers AspNetUsersViewModel = _UserService.(guid);
            //    return View(AspNetUsersViewModel);
            }
            #endregion
        #endregion
    }
}