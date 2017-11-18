using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Models
{
    public class TimeViewModel
    {
        public TimeHeader TimeHeader { get; set; }
        public TimeDetail TimeDetail { get; set; }
        public int DayNo { get; set; }
        public string DateEdit { get; set; }

        public string DateTimeNewAppointment { get; set; }
    }
}