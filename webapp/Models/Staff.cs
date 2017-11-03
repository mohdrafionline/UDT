using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public int PositionID { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public int City { get; set; }
        public int Region { get; set; }
        public int PostalCode { get; set; }

        public int Country { get; set; }
        public long Phone { get; set; }
        public int Extension { get; set; }
        public DateTime TerminationDate { get; set; }
        public string Photo { get; set; }

    }
}