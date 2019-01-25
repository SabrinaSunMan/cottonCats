using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace BackMeow.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// 建立者.
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 更新者.
        /// </summary>
        public string UpdateUser { get; set; }

        /// <summary>
        /// 排序.
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 狀態. False = 刪除,True = 啟用中
        /// </summary>
        public bool Status { get; set; }

        //[DisplayName("建立時間")] //Add
        public DateTime CreateTime { get; set; }

        //[DisplayName("更新時間")] //Add
        public DateTime UpdateTime { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 注意 authenticationType 必須符合 CookieAuthenticationOptions.AuthenticationType 中定義的項目
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在這裡新增自訂使用者宣告
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("StoreEF", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}