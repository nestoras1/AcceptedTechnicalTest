using AcceptedTechnicalTest.Controllers;
using AcceptedTechnicalTest.DataRepository.Classes;
using AcceptedTechnicalTest.DataRepository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Match = AcceptedTechnicalTest.RequestResponses.Match;

namespace AcceptedTechnicalTest.Tests
{
    public class MatchControllerTests
    {
        private readonly MatchController _controller;
        private readonly Mock<ILogger> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDbRepository> _dbRepositoryMock;

        public MatchControllerTests()
        {
            _loggerMock = new Mock<ILogger>();
            _mapperMock = new Mock<IMapper>();
            _dbRepositoryMock = new Mock<IDbRepository>();
            _controller = new MatchController(_loggerMock.Object, _mapperMock.Object, _dbRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllMatchesAsync_ReturnsOkResult()
        {
            // Arrange
            var matches = new List<MatchDao> { new MatchDao() };
            _dbRepositoryMock.Setup(repo => repo.GetAllMatchesAsync()).ReturnsAsync(matches);

            // Act
            var result = await _controller.GetAllMatchesAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetMatchAsync_ReturnsOkResult_WithMatch()
        {
            // Arrange
            var matchId = 1;
            var match = new MatchDao { Id = matchId };
            _dbRepositoryMock.Setup(repo => repo.GetMatchAsync(matchId)).ReturnsAsync(match);

            // Act
            var result = await _controller.GetMatchAsync(matchId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(match, okResult.Value);
        }

        [Fact]
        public async Task AddMatchAsync_ReturnsOkResult_WithAddedMatch()
        {
            // Arrange
            var match = new Match();
            var matchDao = new MatchDao();
            _mapperMock.Setup(mapper => mapper.Map<Match, MatchDao>(match)).Returns(matchDao);
            _dbRepositoryMock.Setup(repo => repo.AddMatchAsync(matchDao)).ReturnsAsync(matchDao);

            // Act
            var result = await _controller.AddMatchAsync(match);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(matchDao, okResult.Value);
        }

        [Fact]
        public async Task UpdateMatchAsync_ReturnsOkResult()
        {
            // Arrange
            var match = new Match();
            var matchDao = new MatchDao();
            _mapperMock.Setup(mapper => mapper.Map<Match, MatchDao>(match)).Returns(matchDao);
            _dbRepositoryMock.Setup(repo => repo.AddMatchAsync(matchDao)).ReturnsAsync(matchDao);

            // Act
            var result = await _controller.UpdateMatchAsync(match, It.IsAny<long>());

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteMatchAsync_ReturnsOkResult_WithDeletedMatchId()
        {
            // Arrange
            var matchId = 1;
            var deletedMatchId = 2;
            _dbRepositoryMock.Setup(repo => repo.DeleteMatchAsync(matchId)).ReturnsAsync(deletedMatchId);

            // Act
            var result = await _controller.DeleteMatchAsync(matchId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal($"Match with id {deletedMatchId} deleted", okResult.Value);
        }
    }
}