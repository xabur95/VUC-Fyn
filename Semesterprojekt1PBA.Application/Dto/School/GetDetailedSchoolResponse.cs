namespace Semesterprojekt1PBA.Application.Dto.School
{
    public record GetDetailedSchoolResponse(
        Guid Id,
        byte[] RowVersion,
        string Title,
        IEnumerable<GetSchoolClassResponse> Classes);
}
