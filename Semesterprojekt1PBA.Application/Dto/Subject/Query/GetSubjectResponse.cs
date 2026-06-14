using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Application.Dto.Subject.Query;

public record GetSubjectResponse(
    Guid Id,
    byte[] RowVersion,
    string Title,
    Level Level
    );