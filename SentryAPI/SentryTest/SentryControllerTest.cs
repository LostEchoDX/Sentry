using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SentryAPI.Controllers;
using SentryAPI.Data;
using SentryAPI.Models;
using SentryAPI.Repositories;
using System;
using System.Data.Entity.Core.Objects;
using System.Text.RegularExpressions;

namespace SentryTest
{
    [TestClass]
    public class SentryControllerTest
    {
        private Mock<IRepository> mockRepo;
        private SentryController controller;
        private PoI checkPoI;
        private Regex latLongEx = new Regex("^((\\-?|\\+?)?\\d+(\\.\\d+)?)$");

        public static DbContextOptions<SentryContext> TestDbContextOptions()
        {
            // Create a new service provider to create a new in-memory database.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance using an in-memory database and 
            // IServiceProvider that the context should resolve all of its 
            // services from.
            var builder = new DbContextOptionsBuilder<SentryContext>()
                .UseInMemoryDatabase("Sentry")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [TestMethod]
        public void GetLineTestMethod()
        {
            mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.GetPoIById(1))
                .Returns(new PoI { drone_id = "a", _class = "civilian", f_id = "friend", picture = "a", 
                    latitude = "48.83940328103467", longitude = "2.3275582914024726" });
            
            controller = new SentryController(mockRepo.Object);
            var result = controller.GetLine(1);
            Assert.IsInstanceOfType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            checkPoI = okResult.Value as PoI;
            StringAssert.Matches(checkPoI.longitude, latLongEx);
            StringAssert.Matches(checkPoI.latitude, latLongEx);
        }
    }
}