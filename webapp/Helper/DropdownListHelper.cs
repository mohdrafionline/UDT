using SmartAdminMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Helpers
{
    public static class DropdownListHelper
    {
        public static List<SelectListItem> GetGenderList()
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem()
            {
                Text = "Male",
                Value = "Male"
            });
            selectItems.Add(new SelectListItem()
            {
                Text = "Female",
                Value = "Female"
            });
            return selectItems;
        }
        public static List<SelectListItem> GetContractorList()
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem()
            {
                Text = "Yes",
                Value = "Yes"
            });
            selectItems.Add(new SelectListItem()
            {
                Text = "No",
                Value = "No"
            });
            return selectItems;
        }
        public static List<SelectListItem> GetCompanyList()
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem()
            {
                Text = "Company1",
                Value = "Company1"
            });
            selectItems.Add(new SelectListItem()
            {
                Text = "Company2",
                Value = "Company2"
            });
            return selectItems;
        }
        public static List<SelectListItem> GetCountryList()
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            DBEntity dBEntity = new DBEntity();
            var res = dBEntity.Country.ToList();
            foreach (var item in res)
            {
                selectItems.Add(new SelectListItem() { Text = item.CountryName, Value = item.CountryID.ToString() });
            }
            return selectItems;
        }

        public static List<SelectListItem> GetWorkList()
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            DBEntity dBEntity = new DBEntity();
            var res = dBEntity.WorkRoles.ToList();
            foreach (var item in res)
            {
                selectItems.Add(new SelectListItem() { Text = item.WorkRoleName, Value = item.WorkRoleID.ToString() });
            }
            return selectItems;
        }
        public static List<SelectListItem> GetBillableList()
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            DBEntity dBEntity = new DBEntity();
            var res = dBEntity.Billables.ToList();
            foreach (var item in res)
            {
                selectItems.Add(new SelectListItem() { Text = item.BillableName, Value = item.BillableID.ToString() });
            }
            return selectItems;
        }

        public static List<SelectListItem> GetDepartmentList()
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            DBEntity dBEntity = new DBEntity();
            var res = dBEntity.Departments.ToList();
            foreach (var item in res)
            {
                selectItems.Add(new SelectListItem() { Text = item.DepartmentName, Value = item.DepartmentID.ToString() });
            }
            return selectItems;
        }
        public static List<SelectListItem> GetStateList(int CountryId)
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            DBEntity dBEntity = new DBEntity();
            var res = dBEntity.State.Where(e => e.CountryID == CountryId).ToList();
            foreach (var item in res)
            {
                selectItems.Add(new SelectListItem() { Text = item.StateName, Value = item.StateID.ToString() });
            }
            return selectItems;
        }
        public static List<SelectListItem> GetCityList(int stateId)
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
            DBEntity dBEntity = new DBEntity();
            var res = dBEntity.City.Where(e => e.StateID == stateId).ToList();
            foreach (var item in res)
            {
                selectItems.Add(new SelectListItem() { Text = item.CityName, Value = item.CityID.ToString() });
            }
            return selectItems;
        }
    }
}