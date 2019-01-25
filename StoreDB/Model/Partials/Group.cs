using System;
using System.ComponentModel.DataAnnotations;

namespace StoreDB.Model.Partials
{
    /// <summary>
    /// 管理者 群組
    /// </summary>
    public partial class Group
    {
        /// <summary>
        /// 管理者群組 ID.
        /// </summary>
        [Key]
        public Guid GroupID { get; set; }

        /// <summary>
        /// 管理者群組 名稱.
        /// </summary>
        [StringLength(10)]
        public string GroupName { get; set; }
    }
}