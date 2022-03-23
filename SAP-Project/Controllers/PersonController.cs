using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entity.Person;
using DataLayer.EnumToPersian;
using DataLayer.Services.EnumService;
using DataLayer.Services.PersonService;
using DataLayer.ViewModel.Person;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAP_Project.Models;
using Utilites;

namespace SAP_Project.Controllers
{
    public class PersonController : Controller
    {
        private IPersonRepository personRepository;
        private IMapper mapper;

        public PersonController(IPersonRepository personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }
        // GET: PersonController
        public async Task<IActionResult> Index(string SearchKey = null, int Page = 1, int PageSize = 20)
        {
            var peapole = await personRepository.GetAllPersonAsync(SearchKey, Page, PageSize);

            #region Use AutoMapper
            ReturnData<IndexPersonViewModel> Result = new ReturnData<IndexPersonViewModel>()
            {
                SearchKey = peapole.SearchKey,
                PageSize = peapole.PageSize,
                CurrentPage = peapole.CurrentPage,
                Data = mapper.Map<List<IndexPersonViewModel>>(peapole.Data),
                DataCount = peapole.DataCount

            };


            #endregion

            return View(Result);
        }



        // GET: PersonController/Details/5
        public async Task<IActionResult> Details(string PersonId)
        {
            if (!string.IsNullOrEmpty(PersonId))
            {
                PersonEntity person = await personRepository.GetPersonByIdAsync(PersonId);
                return View(person);
            }

            ErrorViewModel model = new ErrorViewModel()
            {
                Message = "کاربری یافت نشد",
                Title = "خطا",
                href = "/person/index"
            };
            return View("Error", model);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {

            ViewBag.EducationListItem = EnumSelectList.GetEducationListItem();
            ViewBag.UniversityTypeListItem = EnumSelectList.GetUnivesityTypeListItem();
            return View();
        }


        public bool ValidationMeli(string CodeMelli, string Id)
        {

            if (string.IsNullOrEmpty(Id))
            {
                return (!personRepository.IsExistCodeMelli(CodeMelli)&& IsValidCodeMelli(CodeMelli));
            }

            return (personRepository.IsValidCodeMelli(CodeMelli, Id) && IsValidCodeMelli(CodeMelli));
        }


        public bool IsValidCodeMelli(string CodeMelli)
        {
            try
            {
                var number = Convert.ToInt64(CodeMelli);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }


        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonEntity entity)
        {
            if (ModelState.IsValid)
            {
                entity.InsertDate = DateTime.Now;
                entity.EnterToUniversityDate = entity.EnterToUniversityDate.ToMiladi();
                entity.ExiteFromUniversityDate = entity.ExiteFromUniversityDate.ToMiladi();
                var Result = personRepository.InsertPersonAsync(entity).Result;
                if (Result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ErrorViewModel model = new ErrorViewModel()
                    {
                        Message = Result.Message,
                        Title = "ثبت اطلاعات کاربر با خطا مواجه شد",
                        href = "/person/index"
                    };
                    return View("Error", model);
                }
            }
            ModelState.AddModelError("", "اطلاعات ارسالی نامعتبر است");
            return View(entity);
        }

        [HttpPost]
        public async Task<ReturnResult> DeletePerson(string PersonId)
        {
            return await personRepository.DeletePersonAsync(PersonId);
        }




        // GET: PersonController/Edit/5
        public async Task<IActionResult> Edit(string PersonId)
        {
            if (PersonId != null)
            {
                PersonEntity person = await personRepository.GetPersonByIdAsync(PersonId);
                person.EnterToUniversityDate = person.EnterToUniversityDate.ToShamsi();
                person.ExiteFromUniversityDate = person.ExiteFromUniversityDate.ToShamsi();
                ViewBag.EducationListItem = EnumSelectList.GetEducationListItem();
                ViewBag.UniversityTypeListItem = EnumSelectList.GetUnivesityTypeListItem();
                return View(person);
            }

            return RedirectToAction("Index");
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonEntity person)
        {
            if (ModelState.IsValid)
            {
                person.EnterToUniversityDate = person.EnterToUniversityDate.ToMiladi();
                person.ExiteFromUniversityDate = person.ExiteFromUniversityDate.ToMiladi();
                var result = personRepository.UpdatePersonAsync(person).Result;
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ErrorViewModel model = new ErrorViewModel()
                    {
                        Message = result.Message,
                        Title = "خطای ویرایش اطلاعات کاربر",
                        href = "/person/index"
                    };
                    return View("Error", model);
                }
            }

            ModelState.AddModelError("", "مقادیر ارسالی نامعتبر است");
            return View(person);


        }

        public IActionResult Search()
        {
            ViewBag.StartTime = DateTime.Now.AddYears(-1).ToShamsi();
            ViewBag.EndTime = DateTime.Now.ToShamsi();
            return View();
        }

        public async Task<IActionResult> ResultSearch(SearchFormViewModel model, int Page = 1, int PageSize = 20)
        {
            var Result = await personRepository.SearchInPeapoleAsync(model, Page, PageSize);
            ViewBag.model = model;
            return View(Result);
        }

        public async Task<IActionResult> PersonInfoInSearch(string Id)
        {
            PersonInfoInSearchViewModel model = await personRepository.GetPersonInfoInSearchAsync(Id);
            return View(model);
        }

        public async Task<IActionResult> UniversityInfo(string id)
        {
            var info = await personRepository.GetUniversityInfoAsync(id);
            return View(info);
        }
    }
}
