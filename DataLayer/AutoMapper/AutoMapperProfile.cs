using AutoMapper;
using DataLayer.Entity.Person;
using DataLayer.ViewModel.Person;

namespace DataLayer.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IndexPersonViewModel, PersonEntity>();
            CreateMap<PersonEntity, IndexPersonViewModel>();
        }
    }
}