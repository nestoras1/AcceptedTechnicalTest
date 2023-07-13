using AcceptedTechnicalTest.Controllers;
using AcceptedTechnicalTest.DataRepository.Classes;
using AcceptedTechnicalTest.DataRepository.Interfaces;
using AcceptedTechnicalTest.RequestResponses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AcceptedTechnicalTest.Tests
{
    public class MatchOddsControllerTests
    {
        private readonly Mock<ILogger> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDbRepository> _dbRepositoryMock;
        private readonly MatchOddsController _controller;

        public MatchOddsControllerTests()
        {
            _loggerMock = new Mock<ILogger>();
            _mapperMock = new Mock<IMapper>();
            _dbRepositoryMock = new Mock<IDbRepository>();
            _controller = new MatchOddsController(_loggerMock.Object, _mapperMock.Object, _dbRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllMatchesOddsAsync_ReturnsOkResult()
        {
            // Arrange
            var matchOddsList = new List<MatchOddsDao>();
            _dbRepositoryMock.Setup(repo => repo.GetAllMatchOddsAsync()).ReturnsAsync(matchOddsList);

            // Act
            var result = await _controller.GetAllMatchesOddsAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllMatchesOddsAsync_ReturnsBadRequestError_WhenExceptionThrown()
        {
            // Arrange
            _dbRepositoryMock.Setup(repo => repo.GetAllMatchOddsAsync()).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAllMatchesOddsAsync();

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetMatchOddsAsync_ReturnsOkResult_WithValidMatchOddsId()
        {
            // Arrange
            var matchOddsId = 1L;
            var matchOdds = new MatchOddsDao { Id = matchOddsId };
            _dbRepositoryMock.Setup(repo => repo.GetMatchOddsAsync(matchOddsId)).ReturnsAsync(matchOdds);

            // Act
            var result = await _controller.GetMatchOddsAsync(matchOddsId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetMatchOddsAsync_ReturnsBadRequestError_WhenExceptionThrown()
        {
            // Arrange
            var matchOddsId = 1L;
            _dbRepositoryMock.Setup(repo => repo.GetMatchOddsAsync(matchOddsId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetMatchOddsAsync(matchOddsId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }

        [Fact]
        public async Task AddMatchOddsAsync_ReturnsOkResult_WithValidMatchOdds()
        {
            // Arrange
            var matchOdds = new MatchOdds { MatchId = 1, Specifier = "Olimpiakos", Odd = 1.5 };
            var matchOddsDao = new MatchOddsDao();
            _dbRepositoryMock.Setup(repo => repo.AddMatchOddsAsync(It.IsAny<MatchOddsDao>())).ReturnsAsync(matchOddsDao);

            // Act
            var result = await _controller.AddMatchOddsAsync(matchOdds);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddMatchOddsAsync_ReturnsBadRequestError_WhenExceptionThrown()
        {
            // Arrange
            var matchOdds = new MatchOdds { MatchId = 1, Specifier = "Olimpiakos", Odd = 1.5 };
            _dbRepositoryMock.Setup(repo => repo.AddMatchOddsAsync(It.IsAny<MatchOddsDao>())).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.AddMatchOddsAsync(matchOdds);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }

        [Fact]
        public async Task DeleteMatchOddsAsync_ReturnsOkResult_WithValidMatchOddsId()
        {
            // Arrange
            var matchOddsId = 1L;
            _dbRepositoryMock.Setup(repo => repo.DeleteMatchOddsAsync(matchOddsId)).ReturnsAsync(matchOddsId);

            // Act
            var result = await _controller.DeleteMatchOddsAsync(matchOddsId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteMatchOddsAsync_ReturnsBadRequestError_WhenExceptionThrown()
        {
            // Arrange
            var matchOddsId = 1L;
            _dbRepositoryMock.Setup(repo => repo.DeleteMatchOddsAsync(matchOddsId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.DeleteMatchOddsAsync(matchOddsId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }
    }
}