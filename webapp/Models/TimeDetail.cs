using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Models
{
    public class TimeDetail
    {
        public int TimeDetailID { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }

        public TimeSpan TimeDeduct { get; set; }
        public int WorkTypeID { get; set; }
        public int BillableID { get; set; }
        public string Notes { get; set; }
    }
}