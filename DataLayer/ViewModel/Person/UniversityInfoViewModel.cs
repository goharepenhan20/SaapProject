using System;
using DataLayer.Entity.Person;

namespace DataLayer.ViewModel.Person
{
    public class UniversityInfoViewModel
    {
        public string Id { get; set; }
        public string UniversityName { get; set; }
        public UnivesityType UnivesityType { get; set; }
        public string UniversityAddress { get; set; }
        public DateTime EnterDateToUniversity { get; set; }
        public DateTime ExiteDateFromUniversity { get; set; }
    }
}