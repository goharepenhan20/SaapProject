using DataLayer.Entity.Person;

namespace DataLayer.EnumToPersian
{
    public static class EnumUniversityType
    {
        public static string ToPersianTypeUniversity(this UnivesityType univesity)
        {
            switch (univesity)
            {
                case UnivesityType.Azad:
                    return "آزاد";
                    break;
                case UnivesityType.Dolati:
                    return "دولتی";
                    break;
                case UnivesityType.ext:
                    return "سایر";
                    break;
            }

            return "سایر";
        }
    }
}