using AcceptedTechnicalTest.DataRepository.Classes;
using AcceptedTechnicalTest.DataRepository.Interfaces;
using AcceptedTechnicalTest.RequestResponses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace AcceptedTechnicalTest.Controllers
{
    [ApiController]
    [Route("matchOdds")]
    public class MatchOddsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IDbRepository _dbRepository;

        public MatchOddsController(ILogger logger, IMapper mapper, IDbRepository dbRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _dbRepository = dbRepository;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllMatchesOddsAsync()
        {
            try
            {
                var res = await _dbRepository.GetAllMatchOddsAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to retrieve matches odds with id: {matchOddsId}",
                    nameof(MatchOddsController),
                    nameof(GetAllMatchesOddsAsync));

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetMatchOddsAsync(long id)
        {
            try
            {
                var res = await _dbRepository.GetMatchOddsAsync(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to retrieve matches odds with id: {id}",
                    nameof(MatchOddsController),
                    nameof(GetMatchOddsAsync),
                    id);

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("byMatch/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetMatchOddsByMatchIdAsync(long id)
        {
            try
            {
                var res = await _dbRepository.GetMatchOddsByMatchIdAsync(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to retrieve matches odds with matchId: {matchid}",
                    nameof(MatchOddsController),
                    nameof(GetMatchOddsByMatchIdAsync),
                    id);

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> AddMatchOddsAsync([Required] MatchOdds matchOdds)
        {
            try
            {
                var matchOddsDao = _mapper.Map<MatchOdds, MatchOddsDao>(matchOdds);
                var res = await _dbRepository.AddMatchOddsAsync(matchOddsDao);

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to insert matches odds with matchId: {matchId}",
                    nameof(MatchOddsController),
                    nameof(AddMatchOddsAsync),
                    matchOdds.MatchId);

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateMatchOddsAsync(MatchOdds matchOdds, long id)
        {
            try
            {
                var res = await _dbRepository.UpdateMatchOddsAsync(matchOdds, id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to update matches odds with matchId: {matchId}",
                    nameof(MatchOddsController),
                    nameof(UpdateMatchOddsAsync),
                    matchOdds.MatchId);

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteMatchOddsAsync(long id)
        {
            try
            {
                var res = await _dbRepository.DeleteMatchOddsAsync(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to delete match odds matchOddId: {matchOddId}",
                    nameof(MatchOddsController),
                    nameof(DeleteMatchOddsAsync),
                    id);

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
