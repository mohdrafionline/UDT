using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Models
{
    public class TimeHeader
    {
        public int TimeHeaderID { get; set; }
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public int DivisonID { get; set; }
        public int WorkRoleID { get; set; }
        public string Aggrement { get; set; }
        public DateTime TimeDate { get; set; }
        public int TimeDetailID { get; set; }
        public int AggrementTypeID { get; set; }

        public bool Overnight { get; set; }
    }

    public class TimeDetailsResult
    {
        public string TimeIN { get; set; }
        public string TimeOut { get; set; }

        public string Deduct { get; set; }
        public string Notes { get; set; }
        public int WorkTypeID { get; set; }
        public int BillableID { get; set; }

        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public int DivisonID { get; set; }
        public int WorkRoleID { get; set; }
        public string Aggrement { get; set; }
        public string TimeDate { get; set; }
        public int TimeDetailID { get; set; }
        public int AggrementTypeID { get; set; }

        public bool Overnight { get; set; }
    }
}