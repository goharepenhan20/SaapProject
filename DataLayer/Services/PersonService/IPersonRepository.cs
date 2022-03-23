using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Entity.Person;
using DataLayer.ViewModel.Person;
using Utilites;

namespace DataLayer.Services.PersonService
{
    public interface IPersonRepository
    {
        Task<ReturnData<PersonEntity>> GetAllPersonAsync(string SearchKey, int Page, int PageSize);
        Task<PersonEntity> GetPersonByIdAsync(string PersonId);
        bool IsExistCodeMelli(string CodeMelli);
        bool IsValidCodeMelli(string CodeMelli, string Id);
        Task<ReturnResult> InsertPersonAsync(PersonEntity entity);
        Task<ReturnResult> DeletePersonAsync(string Id);

        Task<ReturnResult> UpdatePersonAsync(PersonEntity person);

        Task<ReturnData<SearchListViewModel>> SearchInPeapoleAsync(SearchFormViewModel model,int page,int pageSize);
        Task<PersonInfoInSearchViewModel> GetPersonInfoInSearchAsync(string PersonId);

        Task<UniversityInfoViewModel> GetUniversityInfoAsync(string PersonId);
    }
}