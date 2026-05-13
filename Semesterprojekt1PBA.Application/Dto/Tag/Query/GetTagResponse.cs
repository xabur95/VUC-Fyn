namespace Semesterprojekt1PBA.Application.Dto.Tag.Query;

public record GetTagResponse(
    Guid Id,
    byte[] RowVersion,
    string Title,
    string Description);
