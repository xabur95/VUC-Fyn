namespace Semesterprojekt1PBA.Application.Dto.School.Query
{
    public record GetDetailedSchoolResponse(
        Guid Id,
        byte[] RowVersion,
        string Title,
        IEnumerable<GetSchoolClassResponse> Classes);
}
