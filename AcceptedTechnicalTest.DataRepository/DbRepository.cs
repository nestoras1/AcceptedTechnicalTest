using AcceptedTechnicalTest.DataRepository.Classes;
using AcceptedTechnicalTest.DataRepository.Interfaces;
using AcceptedTechnicalTest.RequestResponses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcceptedTechnicalTest.DataRepository
{
    public class DbRepository : IDbRepository
    {
        private readonly AcceptedDb_Context _context;


        public DbRepository(AcceptedDb_Context context)
        {
            _context = context;
        }

        public async Task<List<MatchDao>> GetAllMatchesAsync()
        {
            return await _context.Match.ToListAsync();
        }

        public async Task<MatchDao> GetMatchAsync(long matchId)
        {
            return await _context.Match.FirstOrDefaultAsync(x => x.Id == matchId);
        }

        public async Task<MatchDao> AddMatchAsync(MatchDao match)
        {
            await _context.Match.AddAsync(match);
            await _context.SaveChangesAsync();

            return match;
        }

        public async Task<MatchDao> UpdateMatchAsync(Match match, long id)
        {
            var matchRes = await _context.Match.FirstOrDefaultAsync(x => x.Id == id);

            if (matchRes != null)
            {
                matchRes.Description = match.Description;
                matchRes.MatchDate = match.MatchDate;
                matchRes.MatchTime = match.MatchTime;
                matchRes.TeamA = match.TeamA;
                matchRes.TeamB = match.TeamB;
                matchRes.Sport = (int)match.Sport;
                _context.Match.Update(matchRes);
                await _context.SaveChangesAsync();
                return matchRes;
            }
            throw new InvalidOperationException($"Match with ID {id} not found.");
        }

        public async Task<long> DeleteMatchAsync(long matchId)
        {
            var match = await _context.Match.FindAsync(matchId);

            if (match == null)
            {
                throw new InvalidOperationException($"Match with ID {matchId} not found.");
            }

            // Delete associated match_odds records
            var matchOddsToDelete = await _context.MatchOdds.Where(o => o.MatchId == matchId).ToListAsync();
            _context.MatchOdds.RemoveRange(matchOddsToDelete);

            _context.Match.Remove(match);
            await _context.SaveChangesAsync();

            return matchId;
        }

        public async Task<List<MatchOddsDao>> GetAllMatchOddsAsync()
        {
            return await _context.MatchOdds.ToListAsync();
        }

        public async Task<MatchOddsDao> GetMatchOddsAsync(long matchOddId)
        {
            return await _context.MatchOdds.FirstOrDefaultAsync(x => x.Id == matchOddId);
        }

        public async Task<List<MatchOddsDao>> GetMatchOddsByMatchIdAsync(long matchId)
        {
            return await _context.MatchOdds.Where(x => x.MatchId == matchId).ToListAsync();
        }

        public async Task<MatchOddsDao> AddMatchOddsAsync(MatchOddsDao matchOdds)
        {
            await _context.MatchOdds.AddAsync(matchOdds);
            await _context.SaveChangesAsync();

            return matchOdds;
        }

        public async Task<MatchOddsDao> UpdateMatchOddsAsync(MatchOdds matchOdds, long id)
        {
            var matchOddRes = await _context.MatchOdds.FirstOrDefaultAsync(x => x.Id == id);

            if (matchOddRes != null)
            {
                matchOddRes.MatchId = matchOdds.MatchId;
                matchOddRes.Specifier = matchOdds.Specifier;
                matchOddRes.Odd = matchOdds.Odd;

                _context.MatchOdds.Update(matchOddRes);
                await _context.SaveChangesAsync();
                return matchOddRes;
            }

            return matchOddRes;
        }

        public async Task<long> DeleteMatchOddsAsync(long matchOddId)
        {
            var matchOddRes = await _context.MatchOdds.FirstOrDefaultAsync(x => x.Id == matchOddId);

            if (matchOddRes == null)
            {
                throw new InvalidOperationException($"MatchOdd with ID {matchOddRes} not found.");
            }

            _context.MatchOdds.Remove(matchOddRes);
            await _context.SaveChangesAsync();

            return matchOddId;
        }
    }
}
