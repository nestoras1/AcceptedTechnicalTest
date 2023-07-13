using AcceptedTechnicalTest.Common.RequestResponses.Enums;
using AcceptedTechnicalTest.Common.Validators;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcceptedTechnicalTest.DataRepository.Classes
{
    [Table("match")]
    public class MatchDao
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("description")]
        [NoSpecialCharacters]
        public string Description { get; set; }

        [Column("match_date")]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(CustomDateConverter))]
        public DateTime MatchDate { get; set; }

        [Column("match_time")]
        [ValidTime]
        public string MatchTime { get; set; }

        [Column("team_a")]
        [NoSpecialCharacters]
        public string TeamA { get; set; }

        [Column("team_b")]
        [NoSpecialCharacters]
        public string TeamB { get; set; }

        [Column("sport")]
        [EnumDataType(typeof(Sport))]
        public int Sport { get; set; }
    }
}
