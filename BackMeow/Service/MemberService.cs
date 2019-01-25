using BackMeow.App_Start;
using BackMeow.Models.ViewModel;
using PagedList;
using StoreDB.Enum;
using StoreDB.Helper;
using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Model.ViewModel;
using StoreDB.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BackMeow.Service
{
    /// <summary>
    /// 後台 會員管理 相關
    /// </summary>
    public class MemberService
    {
        private readonly MemeberRepository _MemberRep;
        private readonly ZipCodeRepository _ZipCodeRep;

        //private MenuSideListService _menuSideListService;
        public PublicMethodResult ReturnModel;

        private readonly IUnitOfWork _unitOfWork;
        private readonly int pageSize = (int)BackPageListSize.commonSize;

        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _MemberRep = new MemeberRepository(unitOfWork);
            _ZipCodeRep = new ZipCodeRepository(unitOfWork);
            ReturnModel = new PublicMethodResult();
        }

        /// <summary>
        /// Gets the system roles ListView model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <param name="nowpage">The nowpage.</param>
        /// <returns></returns>
        public MemberViewModel GetMemberListViewModel(MemberListHeaderViewModel selectModel, int nowpage = 1)
        {
            MemberViewModel returnMemberListViewModel = new MemberViewModel();
            returnMemberListViewModel.Header = selectModel; /*表頭*/
            IEnumerable<MemberListContentViewModel> GetAllMemberListViewModelResult = GetAllMemberListViewModel(selectModel);
            int currentPage = (nowpage < 1) && GetAllMemberListViewModelResult.Count() >= 1 ? 1 : nowpage;
            returnMemberListViewModel.Content_List = GetAllMemberListViewModelResult.ToPagedList(currentPage, pageSize);/*內容*/
            return returnMemberListViewModel;
        }

        /// <summary>
        /// Gets all member ListView model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <returns></returns>
        private IEnumerable<MemberListContentViewModel> GetAllMemberListViewModel(MemberListHeaderViewModel selectModel)
        {
            // 取得子功能
            IEnumerable<ZipCode> zipcode = _ZipCodeRep.GetAll();
            IEnumerable<Member> members = _MemberRep.GetAll();

            IEnumerable<MemberListContentViewModel> result =
                members.Join(zipcode,
                        s => s.ZipCodeID,
                        z => z.ID,
                        (s, z) => new { s, z })
                        .Where(x =>
                                (!string.IsNullOrEmpty(selectModel.Name) ? x.s.Name.Contains(selectModel.Name) : true)
                                && (!string.IsNullOrWhiteSpace(selectModel.PhoneNumber.ToString()) ?
                                x.s.PhoneNumber.ToString().Contains(selectModel.PhoneNumber.ToString()) : true))
                                .Select(List => new MemberListContentViewModel()
                                {
                                    MemberID = List.s.MemberID,
                                    CreateTime = List.s.CreateTime.ToString(),
                                    Name = List.s.Name,
                                    //Sex = List.Sex.ToString(),
                                    PhoneNumber = List.s.PhoneNumber,
                                    District = List.z.City + " " + List.z.County
                                }).OrderByDescending(s => s.CreateTime).ToList();

            List<MemberListContentViewModel> a = result.ToList();
            return result;
        }

        /// <summary>
        /// Returns the member detail.
        /// </summary>
        /// <param name="ActionType">Type of the action.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public MemberDetailViewModel ReturnMemberDetail(DataAction ActionType, string guid)
        {
            MemberDetailViewModel DetailViewModel = new MemberDetailViewModel();
            Member MemberViewModel = GetMemberById(guid);
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            DetailViewModel = mapper.Map<MemberDetailViewModel>(MemberViewModel);

            ZipCode zipcode = _ZipCodeRep.GetSingle(s => s.ID == MemberViewModel.ZipCodeID);
            //手動綁入
            DetailViewModel.ActionType = ActionType;
            DetailViewModel.ChooseCity = zipcode == null ? "" : zipcode.City;
            DetailViewModel.ChoosePostalCode = zipcode == null ? "" : zipcode.PostalCode.ToString();
            return DetailViewModel;
        }

        /// <summary>
        /// 取得所有前台使用者.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Member> GetAllMember()
        {
            return _MemberRep.GetAll().OrderByDescending(s => s.Name).ToList();
        }

        /// <summary>
        /// Gets the member by identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public Member GetMemberById(string guid)
        {
            return _MemberRep.GetSingle(s => s.MemberID == new System.Guid(guid));
        }

        /// <summary>
        /// Creates the member.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public PublicMethodResult CreateMember(MemberDetailViewModel member, string CreateUser)
        {
            try
            {
                Member PartStaticHtml = ReturnMappingMember(member);

                //SETP 1.確認是否有同樣資料，Email、手機號碼皆不可相同
                var memberInfo = _MemberRep.GetSingle(s => s.Email == PartStaticHtml.Email || s.PhoneNumber == PartStaticHtml.PhoneNumber);
                if (memberInfo == null)
                {
                    _MemberRep.MemberInsertInto(PartStaticHtml, CreateUser);

                    ReturnModel.ResultBool = true;
                    ReturnModel.ActionResult = DataAction.CreateScuess;
                    ReturnModel.Result = EnumHelper.GetEnumDescription(DataAction.CreateScuess);
                }
                else
                {
                    ReturnModel.ResultBool = false;
                    ReturnModel.ActionResult = DataAction.CreateFailReapet;
                    ReturnModel.Result = EnumHelper.GetEnumDescription(DataAction.CreateFailReapet);
                }
            }
            catch (Exception e)
            {
                ReturnModel.ResultBool = false;
                ReturnModel.ActionResult = DataAction.CreateFail;
                ReturnModel.Result = EnumHelper.GetEnumDescription(DataAction.CreateFail);
            }
            return ReturnModel;
        }

        /// <summary>
        /// Update StaticHtml
        /// </summary>
        /// <param name="statichtml"></param>
        /// <returns></returns>
        public PublicMethodResult UpdateMember(MemberDetailViewModel member, string UpdateUser)
        {
            try
            {
                Member PartMember = ReturnMappingMember(member);
                _MemberRep.MemberUpdate(PartMember, UpdateUser);

                ReturnModel.ResultBool = true;
                ReturnModel.ActionResult = DataAction.UpdateScuess;
                ReturnModel.Result = EnumHelper.GetEnumDescription(DataAction.UpdateScuess);
            }
            catch (Exception e)
            {
                ReturnModel.ResultBool = false;
                ReturnModel.ActionResult = DataAction.UpdateFail;
                ReturnModel.Result = EnumHelper.GetEnumDescription(DataAction.UpdateFail);
            }
            return ReturnModel;
        }

        /// <summary>
        /// ViewModel To DBModel
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private Member ReturnMappingMember(MemberDetailViewModel viewModel)
        {
            Member dbViewModel = new Member();
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            dbViewModel = mapper.Map<Member>(viewModel);

            // 手動綁訂, ChoosePostalCode 從 View 回來直接綁中文字，而非原來的 PostalCode
            int choosePostalCode = Convert.ToInt16(viewModel.ChoosePostalCode);
            ZipCode zipcode = _ZipCodeRep.GetSingle(s => s.PostalCode == choosePostalCode);
            dbViewModel.ZipCodeID = zipcode == null ? 0 : zipcode.ID;
            return dbViewModel;
        }

        /// <summary>
        /// 藉由 欄位 與值, 判斷是否能找出資料庫匹配的資料.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool GetMatchBool(string Filed, string MatchValue)
        {
            Member dbViewModel = new Member();
            var memberInfo = _MemberRep.GetSingleMatch(Filed.Trim(), MatchValue.Trim());
            return memberInfo;
            ////object foo = new Member();

            #region 其他寫法

            //var type = foo.GetType();
            //var method = type.GetMethod(Filed);
            //var thisParam = Expression.Parameter(type, "this");

            //var aaa = prop.GetGetMethod();

            //var valueParam = Expression.Parameter(typeof(int), "value");
            //var call = Expression.Call(thisParam, method, valueParam);
            //var lambda = Expression.Lambda<Func<Member, int, string>>(call, thisParam, valueParam);
            //var func = lambda.Compile();
            //var result = func((Member)foo, 42);

            #endregion 其他寫法
        }

        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}