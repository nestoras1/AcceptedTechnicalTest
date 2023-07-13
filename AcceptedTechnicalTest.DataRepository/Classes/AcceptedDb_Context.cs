using Microsoft.EntityFrameworkCore;

namespace AcceptedTechnicalTest.DataRepository.Classes
{
    public class AcceptedDb_Context : DbContext
    {
        public AcceptedDb_Context(DbContextOptions<AcceptedDb_Context> options)
            : base(options) { }

        public virtual DbSet<MatchDao> Match { get; set; }

        public virtual DbSet<MatchOddsDao> MatchOdds { get; set; }
    }
}
