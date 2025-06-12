using KlockorGrupp6App.Application;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Clocks.Services;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockorGrupp6App.Tests.Helpers
{
    public static class TestHelper
    {        
        // Creates a ClockService with mocked IClockRepository and IUnitOfWork.        
        public static ClockService CreateClockServiceWithMocks(out Mock<IClockRepository> mockClockRepo, out Mock<IUnitOfWork> mockUnit)
        {
            mockClockRepo = new Mock<IClockRepository>();
            mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(u => u.Clocks).Returns(mockClockRepo.Object);

            return new ClockService(mockUnit.Object);
        }
        
        // Creates a mocked UserManager for ApplicationUser.       
        public static Mock<UserManager<ApplicationUser>> CreateMockUserManager()
        {
            var mockStore = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                mockStore.Object,   // IUserStore<ApplicationUser> store
                null,               // IOptions<IdentityOptions> 
                null,               // IPasswordHasher<ApplicationUser>  
                null,               // IEnumerable<IPasswordHasher<ApplicationUser>>  
                null,               // IEnumerable<IPasswordValidator<ApplicationUser>>  
                null,               // IEnumerable<IUserValidator<ApplicationUser>>  
                null,               // ILookupNormalizer  
                null,               // IServiceProvider  
                null                // ILogger<UserManager<ApplicationUser>> 
                );
        }
        
        // Creates a ClocksController with mocked dependencies.        
        public static ClocksController CreateClocksController(
            out Mock<IClockService> mockClockService,
            out Mock<UserManager<ApplicationUser>> mockUserManager)
        {
            mockClockService = new Mock<IClockService>();
            mockUserManager = CreateMockUserManager();
            return new ClocksController(mockClockService.Object, mockUserManager.Object);
        }
    }
}