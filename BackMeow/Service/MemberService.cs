using BackMeow.Models.ViewModel;
using PagedList;
using StoreDB.Model.Partials;
using StoreDB.Enum;
using StoreDB.Repositories;
using System.Collections.Generic;
using System.Linq;
using StoreDB.Interface;
using BackMeow.App_Start;

namespace BackMeow.Service
{
    /// <summary>
    /// 後台 會員管理 相關
    /// </summary>
    public class MemberService
    {
        private readonly MemeberRepository _MemberRep;
        private MenuSideListService _menuSideListService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly int pageSize = (int)BackPageListSize.commonSize;

        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _MemberRep = new MemeberRepository(unitOfWork);
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
            IEnumerable<MemberListContentViewModel> result =
                _MemberRep.GetAll().Where(s => s.Status == true).Where(s => (!string.IsNullOrEmpty(selectModel.Name) ?
                                        s.Name.Contains(selectModel.Name) : true)
                                            && (!string.IsNullOrWhiteSpace(selectModel.PhoneNumber.ToString()) ?
                                        s.PhoneNumber.ToString().Contains(selectModel.PhoneNumber.ToString()) : true)
                                            && (!string.IsNullOrWhiteSpace(selectModel.CreateTime.ToString()) ?
                                        s.CreateTime.ToString().Contains(selectModel.CreateTime.ToString()) : true)
                                    ).Select(List => new MemberListContentViewModel()
                                    {
                                        MemberID = List.MemberID,
                                        CreateTime = List.CreateTime.ToString(),
                                        Name = List.Name,
                                        Sex = List.Sex.ToString()
                                    }).OrderByDescending(s => s.CreateTime).ToList();
            return result;
        }

        /// <summary>
        /// Returns the member detail.
        /// </summary>
        /// <param name="ActionType">Type of the action.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public MemberDetailViewModel ReturnMemberDetail(Actions ActionType, string guid)
        {
            MemberDetailViewModel DetailViewModel = new MemberDetailViewModel();
            Member MemberViewModel = GetMemberById(guid);
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            DetailViewModel = mapper.Map<MemberDetailViewModel>(MemberViewModel);

            //手動綁入
            DetailViewModel.ActionType = ActionType;
            return DetailViewModel;
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
        public string CreateMember(MemberDetailViewModel member, string userName)
        {
            try
            {
                Member PartStaticHtml = ReturnMappingMember(member);
                _MemberRep.MemberInsertInto(PartStaticHtml, userName);

                return EnumHelper.GetEnumDescription(DataAction.CreateScuess);
            }
            catch
            {
                return EnumHelper.GetEnumDescription(DataAction.CreateFail);
            }
        }

        /// <summary>
        /// Update StaticHtml
        /// </summary>
        /// <param name="statichtml"></param>
        /// <returns></returns>
        public string UpdateMember(MemberDetailViewModel member, string userName)
        {
            try
            {
                Member PartMember = ReturnMappingMember(member);
                _MemberRep.MemberUpdate(PartMember, userName);
                return EnumHelper.GetEnumDescription(DataAction.UpdateScuess);
            }
            catch
            {
                return EnumHelper.GetEnumDescription(DataAction.UpdateFail);
            }
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
            return dbViewModel;
        }

        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}