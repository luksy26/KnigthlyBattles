namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record PlayerUpdateDTO(Guid Id, string? Name = default, int? Rating = default!, int? Age = default!);
