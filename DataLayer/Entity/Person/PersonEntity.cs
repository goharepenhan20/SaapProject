using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace DataLayer.Entity.Person
{
    public class PersonEntity
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
        [Display(Name = "نام")][Required(ErrorMessage = "لطفا{0} را وارد کنید")][MaxLength(150)]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")][Required(ErrorMessage = "لطفا{0} را وارد کنید")][MaxLength(250)]
        public string LastName { get; set; }
        [Display(Name = "کد ملی")][Required(ErrorMessage = "لطفا{0} را وارد کنید")][MaxLength(10,ErrorMessage = "کد ملی باید شامل 10 کارکتر عددی باشد")][MinLength(10, ErrorMessage = "کد ملی باید شامل 10 کارکتر باشد")][Remote("ValidationMeli","Person",ErrorMessage = "این کد ملی قبلا ثبت شده است یا معتبر نیست",AdditionalFields = "Id")]
        public string CodeMelli { get; set; }
        [Display(Name = "مقطع تحصیلی")]
        public Education Education { get; set; }
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [Display(Name = "رشته تحصیلی")][MaxLength(150)]
        public string Reshteh { get; set; }
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [Display(Name = "گرایش تحصیلی")][MaxLength(250)]
        public string Gerayesh { get; set; }
        [Display(Name = "معدل")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public decimal Moaadel { get; set; }
        [Display(Name = "نام دانشگاه")][MaxLength(150)]
        public string UniversityName { get; set; }
        [Display(Name = "نوع دانشگاه")]
        public UnivesityType UnivesityType { get; set; }
        [Display(Name = "آدرس دانشگاه")][MaxLength(1500)]
        public string UniversityAddress { get; set; }
        [Display(Name = "تاریخ ورود به دانشگاه")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public DateTime EnterToUniversityDate { get; set; }
        [Display(Name = "تاریخ اتمام دانشگاه")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public DateTime ExiteFromUniversityDate { get; set; }
        [Display(Name = "تاریخ ثبت اطلاعات")]

        public DateTime InsertDate { get; set; }

    }

    public enum Education
    {
        sikle,diplom,foghdiplom,lisans,foghlisans,doctor,foghdoctora
    }

    public enum UnivesityType
    {
        Azad,Dolati,ext
    }
}