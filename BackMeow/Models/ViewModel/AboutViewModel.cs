using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackMeow.Models.ViewModel
{
    /// <summary>
    /// 靜態網頁_關於
    /// </summary>
    public class AboutViewModel
    {
        /// <summary>
        /// 類別名稱.
        /// </summary> 
        [DisplayName("類別名稱")]
        [StringLength(20, ErrorMessage = "長度不得超過{0}")]
        public string SubjectName { get; set; }
    }
}