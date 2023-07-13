using AcceptedTechnicalTest.Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace AcceptedTechnicalTest.RequestResponses
{
    public class MatchOdds
    {
        public long MatchId { get; set; }

        [Required]
        [NoSpecialCharacters]
        public string Specifier { get; set; }

        [Required]
        [OddValidator(ErrorMessage = "The Odd value must be greater than 1.")]
        public double Odd { get; set; }
    }
}
