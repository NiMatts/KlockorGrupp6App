using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Clocks.Services;
using KlockorGrupp6App.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockorGrupp6App.Tests.Application
{
    public class ClockServiceTests
    {
        #region [Test Setup]
        private readonly Mock<IClockRepository> _mockClockRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork; // UnitOfWork still needs to be implemented        
        private readonly ClockService _service;        
                
        public ClockServiceTests()
        {
            _mockClockRepository = new Mock<IClockRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.Clocks).Returns(_mockClockRepository.Object);
            _service = new ClockService(_mockUnitOfWork.Object);

        }
        #endregion

        //Still a work in progress. Need to fix Async methods and implement UnitOfWork before getting things in order.
    }
}
