using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SmartAdminMvc.DBModel;

namespace SmartAdminMvc.Models
{
    public class DBEntity : DbContext
    {
        public DbSet<tblUser> tblUsers { get; set; }
        public DbSet<tblEmployees> TblEmployees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<TimeDetail> TimeDetails { get; set; }
        public DbSet<TimeHeader> TimeHeaders { get; set; }
        public DbSet<WorkRole> WorkRoles { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<Billable> Billables { get; set; }
        public DbSet<AggrementType> AggrementTypes { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
    }

   
}