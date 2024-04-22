namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record PlayerUpdateDTO(Guid Id, string? Name = default, int? Rating = 0, int? Age = 0);
