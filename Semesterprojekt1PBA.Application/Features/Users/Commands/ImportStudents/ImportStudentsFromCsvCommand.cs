using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.ImportStudents;
/// <summary>
/// Author: Michael
/// Repræsenterer en kommando til import af studerende fra en CSV-fil via en læsbar stream.
/// Filen skal overholde det forventede format. Operationen er transaktionel – enten importeres alle poster, eller ingen.
/// </summary>

public record ImportStudentsFromCsvCommand : IRequest<Unit>, ITransactionalCommand
{
    public Stream CsvFile { get; init; } = null!;
}