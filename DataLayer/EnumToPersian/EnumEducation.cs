using DataLayer.Entity.Person;

namespace DataLayer.EnumToPersian
{
    public static class EnumEducation
    {
        public static string ToPersianEducation(this Education education)
        {
            switch (education)
            {
                case Education.diplom:
                    return "دیپلم";
                    break;
                case Education.doctor:
                    return "دکترا";
                    break;

                case Education.foghdiplom:
                    return "فوق دیپلم";
                    break;

                case Education.foghdoctora:
                    return "فوق دکترا";
                    break;

                case Education.foghlisans:
                    return "فوق لیسانس";
                    break;

                case Education.lisans:
                    return "لیسانس";
                    break;

                case Education.sikle:
                    return "سیکل";
                    break;


            }

            return "هیچکدام";
        }
    }
}