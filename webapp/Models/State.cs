namespace SmartAdminMvc.DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    {
        public int StateID { get; set; }

        public int CountryID { get; set; }

        [StringLength(200)]
        public string StateName { get; set; }

        public virtual Country Country { get; set; }
    }
}
