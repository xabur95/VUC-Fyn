namespace Semesterprojekt1PBA.Application.Dto.Class.Query;

public record GetAllClassesResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public int StudentCount { get; init; }
}
