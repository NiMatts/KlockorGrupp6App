using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Domain;

namespace KlockorGrupp6App.Application.Clocks.Services;

//public class ClockService(IClockRepository clockRepository) : IClockService
public class ClockService(IUnitOfWork unitOfWork) : IClockService
{
    public async Task AddAsync(Clock clock)
    {
        await unitOfWork.Clocks.AddAsync(clock);
        await unitOfWork.PersistAllAsync();
    }
    public async Task RemoveAsync(Clock clock)
    {
        unitOfWork.Clocks.Remove(clock);
        await unitOfWork.PersistAllAsync();
    }
    public async Task<Clock[]> GetAllAsync()
    {
        return await unitOfWork.Clocks.GetAllAsync();
    }

    public async Task<Clock?> GetByIdAsync(int id)
    {
        return await unitOfWork.Clocks.GetByIdAsync(id);
    }
    public async Task<Clock[]?> GetAllByUserId(string id)
    {
        return await unitOfWork.Clocks.GetAllByUserIdAsync(id);
    }
}
