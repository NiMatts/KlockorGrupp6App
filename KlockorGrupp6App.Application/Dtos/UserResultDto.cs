namespace KlockorGrupp6App.Application.Dtos;

public record UserResultDto(string? ErrorMessage)
{
    public bool Succeeded => ErrorMessage == null;
}
