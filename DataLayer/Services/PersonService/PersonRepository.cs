using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Entity.Person;
using DataLayer.ViewModel.Person;
using Microsoft.EntityFrameworkCore;
using Utilites;

namespace DataLayer.Services.PersonService
{
    public class PersonRepository : IPersonRepository
    {
        private SAPCOntext contaxt;

        public PersonRepository(SAPCOntext contaxt)
        {
            this.contaxt = contaxt;
        }

        /// <summary>
        /// دریافت لیست تمامی کاربران بر اساس صفحه بندی و جستجو
        /// </summary>
        /// <param name="SearchKey">کلمه قایل جستجو</param>
        /// <param name="Page">شماره صفحه</param>
        /// <param name="PageSize">تعداد رکرود در هر صفحه</param>
        /// <returns></returns>
        public async Task<ReturnData<PersonEntity>> GetAllPersonAsync(string SearchKey, int Page, int PageSize)
        {
            var Peapole = contaxt.PersonEntities.OrderByDescending(p => p.InsertDate).AsQueryable();
            if (!string.IsNullOrEmpty(SearchKey))
            {
                Peapole = Peapole.Where(p =>
                    p.CodeMelli.Contains(SearchKey) || p.LastName.Contains(SearchKey) ||
                    p.Gerayesh.Contains(SearchKey) || p.Name.Contains(SearchKey) || p.Reshteh.Contains(SearchKey) ||
                    p.UniversityName.Contains(SearchKey) || p.UniversityAddress.Contains(SearchKey));
            }

            return Peapole.ToPage(Page, PageSize);
        }


        /// <summary>
        /// دریافت اطلاعات یک کاربر بر اساس شناسه کاربری
        /// </summary>
        /// <param name="PersonId">شناسه کاربر</param>
        /// <returns></returns>
        public async Task<PersonEntity> GetPersonByIdAsync(string PersonId)
        {
            return await contaxt.PersonEntities.FirstOrDefaultAsync(p => p.Id == PersonId);
        }


        /// <summary>
        /// بررسی اینکه کد ملی در بانک اطلاعاتی موجود است یا خیر
        /// </summary>
        /// <param name="CodeMelli">کدملی</param>
        /// <returns></returns>
        public bool IsExistCodeMelli(string CodeMelli)
        {
            return contaxt.PersonEntities.Any(p => p.CodeMelli == CodeMelli);
        }


        /// <summary>
        /// آیا این کد ملی برای این شناسه معتبر است یا خیر
        /// </summary>
        /// <param name="CodeMelli">کد ملی</param>
        /// <param name="Id">شناسه</param>
        /// <returns></returns>
        public bool IsValidCodeMelli(string CodeMelli, string Id)
        {
            var person = contaxt.PersonEntities.FirstOrDefault(p => p.CodeMelli == CodeMelli);
            if (person == null)
            {
                return true;
            }

            if (person.Id == Id)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// افزودن یک کاربر به بانک
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ReturnResult> InsertPersonAsync(PersonEntity entity)
        {
            try
            {
                await contaxt.PersonEntities.AddAsync(entity);
                await contaxt.SaveChangesAsync();
                return new ReturnResult()
                {
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ReturnResult()
                {
                    IsSuccess = false,
                    Message = e.ToString()
                };
            }
        }

        /// <summary>
        /// حذف یک کاربر بر اساس شناسه آن
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ReturnResult> DeletePersonAsync(string Id)
        {
            PersonEntity person = await GetPersonByIdAsync(Id);
            if (person != null)
            {
                try
                {
                    contaxt.PersonEntities.Remove(person);
                    await contaxt.SaveChangesAsync();
                    return new ReturnResult()
                    {
                        Message = "عملیات با موفقیت انجام شد",
                        IsSuccess = true
                    };
                }
                catch (Exception e)
                {
                    return new ReturnResult()
                    {
                        Message = "عملیات با شکست مواجه شد",
                        IsSuccess = true
                    };
                }
            }

            return new ReturnResult()
            {
                Message = "کاربری یافت نشد",
                IsSuccess = false
            };
        }


        /// <summary>
        /// ویرایش اطلاعات یک کاربر
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task<ReturnResult> UpdatePersonAsync(PersonEntity person)
        {
            try
            {
                contaxt.PersonEntities.Update(person);
                await contaxt.SaveChangesAsync();
                return new ReturnResult()
                {
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ReturnResult()
                {
                    IsSuccess = false,
                    Message = e.ToString()
                };
            }
        }


        /// <summary>
        /// جستجو در لیست کاربران بر اساس بازه تاریخی فارغ التحصیلی و کدملی و نام و نام خانوادگی
        /// </summary>
        /// <param name="Fullname"></param>
        /// <param name="CodeMelli"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public async Task<ReturnData<SearchListViewModel>> SearchInPeapoleAsync(SearchFormViewModel model, int page, int pageSize)
        {
            #region تبدیل تاریخ از شمسی به میلادی

            model.StartDate = model.StartDate.ToMiladi().AddDays(-1);
            model.EndDate = model.EndDate.ToMiladi().AddDays(1);

            #endregion

            #region تبدیل تاریخ اول به ساعت اولیه
            var StartDateTime = model.StartDate.ToEndDay();

            #endregion

            #region تبدیل تاریخ دوم به ساعت های پایانی
            var EndDateTime = model.EndDate.ToStartDay();
            #endregion

            #region عملیات فیلتر بر اساس تاریخ
            var peapole = contaxt.PersonEntities
                           .Where(p => p.ExiteFromUniversityDate > StartDateTime && p.ExiteFromUniversityDate < EndDateTime)
                           .AsQueryable();
            #endregion

            #region فیلتر بر اساس کدملی و نام و تام خانوادگی

            if (!string.IsNullOrEmpty(model.CodeMelli))
            {
                peapole = peapole.Where(p => p.CodeMelli == model.CodeMelli);
            }

            if (!string.IsNullOrEmpty(model.Fullname))
            {
                peapole = peapole.Where(p => (p.Name + " " + p.LastName).Contains(model.Fullname));
            }


            #endregion



            List<SearchListViewModel> SearchResult = new List<SearchListViewModel>();
            SearchResult = peapole.Select(p => new SearchListViewModel()
            {
                LastName = p.LastName,
                Name = p.Name,
                Id = p.Id,
                Insertdate = p.InsertDate,
                CodeMelli = p.CodeMelli
            }).ToList();


            return SearchResult.ToPage(page, pageSize);
        }


        /// <summary>
        /// اطلاعات کاربر بعد از جستجو و کلیک بر روی نام خانوادگی
        /// </summary>
        /// <param name="PersonId"></param>
        /// <returns></returns>
        public async Task<PersonInfoInSearchViewModel> GetPersonInfoInSearchAsync(string PersonId)
        {
            return await contaxt.PersonEntities.Where(p => p.Id == PersonId).Select(d => new PersonInfoInSearchViewModel()
            {
                Education = d.Education,
                Fullname = d.Name + " " + d.LastName,
                Gerayesh = d.Gerayesh,
                Id = d.Id,
                UniversityName = d.UniversityName,
                Moaadel = d.Moaadel,
                Reshteh = d.Reshteh
            }).FirstOrDefaultAsync();
        }

        public async Task<UniversityInfoViewModel> GetUniversityInfoAsync(string PersonId)
        {
            return await contaxt.PersonEntities.Where(p => p.Id == PersonId).Select(d => new UniversityInfoViewModel()
            {
                EnterDateToUniversity = d.EnterToUniversityDate,
                ExiteDateFromUniversity = d.ExiteFromUniversityDate,
                Id = d.Id,
                UnivesityType = d.UnivesityType,
                UniversityAddress = d.UniversityAddress,
                UniversityName = d.UniversityName
            }).FirstOrDefaultAsync();
        }
    }
}