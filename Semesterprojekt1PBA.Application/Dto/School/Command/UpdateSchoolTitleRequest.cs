namespace Semesterprojekt1PBA.Application.Dto.School.Command;

public record UpdateSchoolTitleRequest(
    Guid Id,
    byte[] RowVersion,
    string Title
    );

