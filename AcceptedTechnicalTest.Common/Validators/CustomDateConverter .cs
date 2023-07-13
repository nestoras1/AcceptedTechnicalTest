using Newtonsoft.Json.Converters;

namespace AcceptedTechnicalTest.Common.Validators
{
    public class CustomDateConverter : IsoDateTimeConverter
    {
        public CustomDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
