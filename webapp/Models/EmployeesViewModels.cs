using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Models
{
    public class EmployeesViewModels
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string EmailID { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

    }

    public class tblEmployees
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string EmailID { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

    }
}