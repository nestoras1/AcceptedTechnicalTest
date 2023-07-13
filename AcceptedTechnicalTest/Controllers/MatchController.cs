using AcceptedTechnicalTest.DataRepository.Classes;
using AcceptedTechnicalTest.DataRepository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using Match = AcceptedTechnicalTest.RequestResponses.Match;

namespace AcceptedTechnicalTest.Controllers
{
    [ApiController]
    [Route("match")]
    public class MatchController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IDbRepository _dbRepository;

        public MatchController(ILogger logger, IMapper mapper, IDbRepository dbRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _dbRepository = dbRepository;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllMatchesAsync()
        {
            try
            {
                var res = await _dbRepository.GetAllMatchesAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to retrieve all matches",
                    nameof(MatchController),
                    nameof(GetAllMatchesAsync));

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetMatchAsync(long id)
        {
            try
            {
                var res = await _dbRepository.GetMatchAsync(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to retrieve match with id: {matchId}",
                    nameof(MatchController),
                    nameof(GetMatchAsync),
                    id);

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> AddMatchAsync([FromBody] Match match)
        {
            try
            {
                var matchDao = _mapper.Map<Match, MatchDao>(match);
                await _dbRepository.AddMatchAsync(matchDao);

                return Ok(matchDao);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to insert match.",
                    nameof(MatchController),
                    nameof(AddMatchAsync));

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateMatchAsync([FromBody] Match match, long id)
        {
            try
            {
                await _dbRepository.UpdateMatchAsync(match, id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to update match with id {matchId}.",
                    nameof(MatchController),
                    nameof(UpdateMatchAsync));

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteMatchAsync(long id)
        {
            try
            {
                var deletedMatchId = await _dbRepository.DeleteMatchAsync(id);

                return Ok($"Match with id {deletedMatchId} deleted");
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "{class}.{method} Not able to delete match with id {matchId}.",
                    nameof(MatchController),
                    nameof(DeleteMatchAsync),
                    id);

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
