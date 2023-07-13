using AcceptedTechnicalTest.DataRepository.Classes;
using AcceptedTechnicalTest.RequestResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcceptedTechnicalTest.DataRepository.Interfaces
{
    public interface IDbRepository
    {
        Task<List<MatchDao>> GetAllMatchesAsync();

        Task<MatchDao> GetMatchAsync(long matchId);

        Task<MatchDao> AddMatchAsync(MatchDao match);

        Task<MatchDao> UpdateMatchAsync(Match match, long id);

        Task<long> DeleteMatchAsync(long matchId);

        Task<List<MatchOddsDao>> GetAllMatchOddsAsync();

        Task<MatchOddsDao> GetMatchOddsAsync(long matchOddId);

        Task<List<MatchOddsDao>> GetMatchOddsByMatchIdAsync(long matchId);

        Task<MatchOddsDao> AddMatchOddsAsync(MatchOddsDao matchOdds);

        Task<MatchOddsDao> UpdateMatchOddsAsync(MatchOdds matchOdds, long id);

        Task<long> DeleteMatchOddsAsync(long matchOddId);
    }
}
