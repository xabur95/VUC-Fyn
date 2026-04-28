namespace Semesterprojekt1PBA.Application.Dto.Tag.Command;

public record UpdateTagRequest(
    Guid TagId,
    byte[] RowVersion,
    string Title,
    string Description);
