using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Models
{
    public class TimeSheet
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public String Status { get; set; }
        public double Hours { get; set; }
        public DateTime DeadLine { get; set; }
        public TimeSpan Day1 { get; set; }
        public TimeSpan Day2 { get; set; }
        public TimeSpan Day3 { get; set; }
        public TimeSpan Day4 { get; set; }
        public TimeSpan Day5 { get; set; }
        public TimeSpan Day6 { get; set; }
        public TimeSpan Day7 { get; set; }
        
    }
}