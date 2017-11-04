using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }

        }
        public string Gender { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public int PostalCode { get; set; }
        public int Country { get; set; }
        public string Contractor { get; set; }

        public byte[] W9Form { get; set; }

        public byte[] StaffPhoto { get; set; }

        public string StaffNumber { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentID { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string MobilePhone { get; set; }
        public string BirthDate { get; set; }
        public string Title { get; set; }
        public DateTime? HireDate { get; set; }
        public long Phone { get; set; }
        public int Extension { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Photo { get; set; }

    }
}