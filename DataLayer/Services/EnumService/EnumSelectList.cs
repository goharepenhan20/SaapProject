using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Entity.Person;
using DataLayer.EnumToPersian;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataLayer.Services.EnumService
{
    public static class EnumSelectList
    {
        public static List<SelectListItem> GetEducationListItem()
        {
            var EducationList = Enum.GetValues(typeof(Education)).Cast<Education>().ToList();
            List<SelectListItem> EducationItems = new List<SelectListItem>();
            foreach (var item in EducationList)
            {
                EducationItems.Add(new SelectListItem(item.ToPersianEducation(), item.ToString()));
            }

            return EducationItems;
        }

        public static List<SelectListItem> GetUnivesityTypeListItem()
        {
            var EducationList = Enum.GetValues(typeof(UnivesityType)).Cast<UnivesityType>().ToList();
            List<SelectListItem> EducationItems = new List<SelectListItem>();
            foreach (var item in EducationList)
            {
                EducationItems.Add(new SelectListItem(item.ToPersianTypeUniversity(), item.ToString()));
            }

            return EducationItems;
        }



    }
}