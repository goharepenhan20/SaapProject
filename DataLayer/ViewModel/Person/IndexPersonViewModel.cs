using System;
using DataLayer.Entity.Person;

namespace DataLayer.ViewModel.Person
{
    public class IndexPersonViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Education Education { get; set; }
        public string Reshteh { get; set; }
        public string UniversityName { get; set; }
        public DateTime EnterToUniversityDate { get; set; }
        public DateTime ExiteFromUniversityDate { get; set; }
        public DateTime InsertDate { get; set; }
    }
}