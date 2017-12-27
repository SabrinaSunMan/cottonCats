namespace StoreDB.Model.Partials
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Students
    {
        [Key]
        public int studentID { get; set; }

        public string studentName { get; set; }

        public int? addressInfo_AddressID { get; set; }

        public virtual Addresses Addresses { get; set; }
    }
}
