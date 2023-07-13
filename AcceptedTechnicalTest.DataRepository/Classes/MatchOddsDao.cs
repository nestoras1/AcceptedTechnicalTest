using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcceptedTechnicalTest.DataRepository.Classes
{
    [Table("match_odds")]
    public class MatchOddsDao
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("match_id")]
        public long MatchId { get; set; }

        [Column("specifier")]
        public string Specifier { get; set; }

        [Column("odd")]
        public double Odd { get; set; }
    }
}
