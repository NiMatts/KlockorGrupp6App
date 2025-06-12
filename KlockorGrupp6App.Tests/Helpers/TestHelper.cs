using KlockorGrupp6App.Tests.Web;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Clocks.Services;
using KlockorGrupp6App.Application;
using KlockorGrupp6App.Domain;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Web.Controllers;
using KlockorGrupp6App.Web.Views.Klockor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using KlockorGrupp6App.Tests.Application;
using KlockorGrupp6App.Tests;
using KlockorGrupp6App.Application.Users;

namespace KlockorGrupp6App.Tests.Helpers
{
    public static class TestHelper
    {        
        // Creates a ClockService with mocked IClockRepository and IUnitOfWork.        
        public static ClockService CreateClockServiceWithMocks(
            out Mock<IClockRepository> mockClockRepo, 
            out Mock<IUnitOfWork> mockUnit)
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
                default!,               // IOptions<IdentityOptions> 
                default!,               // IPasswordHasher<ApplicationUser>  
                default!,               // IEnumerable<IPasswordHasher<ApplicationUser>>  
                default!,               // IEnumerable<IPasswordValidator<ApplicationUser>>  
                default!,               // IEnumerable<IUserValidator<ApplicationUser>>  
                default!,               // ILookupNormalizer  
                default!,               // IServiceProvider  
                default!                // ILogger<UserManager<ApplicationUser>> 
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

        // Creates an AccountController with a mocked UserManager.
        public static AccountController CreateAccountController(
            out Mock<UserManager<ApplicationUser>> mockUserManager,
            out Mock<IUserService> mockUserService,
            out Mock<IClockService> mockClockService)
        {
            mockUserManager = CreateMockUserManager();
            mockUserService = new Mock<IUserService>();
            mockClockService = new Mock<IClockService>();

            return new AccountController(
                mockUserService.Object,
                mockUserManager.Object,                
                mockClockService.Object);
        }
    }    
}