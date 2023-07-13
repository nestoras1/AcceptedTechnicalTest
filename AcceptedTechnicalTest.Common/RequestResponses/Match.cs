using AcceptedTechnicalTest.Common.RequestResponses.Enums;
using AcceptedTechnicalTest.Common.Validators;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AcceptedTechnicalTest.RequestResponses
{
    public class Match
    {
        [Required]
        [NoSpecialCharacters]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(CustomDateConverter))]
        public DateTime MatchDate { get; set; }

        [Required]
        [ValidTime]
        public string MatchTime { get; set; }

        [Required]
        [NoSpecialCharacters]
        public string TeamA { get; set; }

        [Required]
        [NoSpecialCharacters]
        public string TeamB { get; set; }

        [Required]
        [EnumDataType(typeof(Sport))]
        public Sport Sport { get; set; }
    }
}
