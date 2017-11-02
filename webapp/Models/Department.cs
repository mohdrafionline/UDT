using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }
}