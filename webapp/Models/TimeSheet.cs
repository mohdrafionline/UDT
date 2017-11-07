using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Models
{
    public class TimeSheet
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public DateTime DateFrom { get; set; }
        [NotMapped]
        public string DateFromJ { get; set; }
        public DateTime DateTo { get; set; }
        [NotMapped]
        public string DateToJ { get; set; }
        public String Status { get; set; }
        public TimeSpan Hours { get; set; }
        [NotMapped]
        public string HoursJ { get; set; }
        public DateTime DeadLine { get; set; }
        [NotMapped]
        public string DeadLineJ { get; set; }
        public TimeSpan Day1 { get; set; }
        public TimeSpan Day2 { get; set; }
        public TimeSpan Day3 { get; set; }
        public TimeSpan Day4 { get; set; }
        public TimeSpan Day5 { get; set; }
        public TimeSpan Day6 { get; set; }
        public TimeSpan Day7 { get; set; }
        [NotMapped]
        public string Day1J { get; set; }
        [NotMapped]
        public string Day2J { get; set; }
        [NotMapped]
        public string Day3J { get; set; }
        [NotMapped]
        public string Day4J { get; set; }
        [NotMapped]
        public string Day5J { get; set; }
        [NotMapped]
        public string Day6J { get; set; }
        [NotMapped]
        public string Day7J { get; set; }
        [NotMapped]
        public int UserID { get; set; }

    }
}