namespace StoreDB.Model.Partials
{
    using System.ComponentModel.DataAnnotations;

    public partial class Students
    {
        [Key]
        public int studentID { get; set; }

        public string studentName { get; set; }

        public int? addressInfo_AddressID { get; set; }

        public virtual Addresses Addresses { get; set; }
    }
}
