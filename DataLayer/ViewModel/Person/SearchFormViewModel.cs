using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace DataLayer.ViewModel.Person
{
    public class SearchFormViewModel
    {
        [Display(Name = "فارغ التحصیل از تاریخ")][Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public DateTime StartDate { get; set; }
        [Display(Name = "فارغ التحصیل تا تاریخ")][Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public DateTime EndDate { get; set; }
        [Display(Name = "کد ملی")][MaxLength(10,ErrorMessage = "کد ملی باید شامل 10 کارکتر عددی باشد")][MinLength(10, ErrorMessage = "کد ملی باید شامل 10 کارکتر عددی باشد")][Remote("IsValidCodeMelli","Person",ErrorMessage = "کد ملی فقط شامل عدد است")]
        public string CodeMelli { get; set; }
        [Display(Name = "نام ونام خانوادگی")]
        public string Fullname { get; set; }

    }
}