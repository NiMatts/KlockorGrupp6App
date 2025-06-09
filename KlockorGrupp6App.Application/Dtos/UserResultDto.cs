using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockorGrupp6App.Application.Dtos;

public record UserResultDto(string? ErrorMessage)
{
    public bool Succeeded => ErrorMessage == null;
}
