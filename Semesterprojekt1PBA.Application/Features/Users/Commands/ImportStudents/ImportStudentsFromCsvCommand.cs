using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.ImportStudents;
/// <summary>
/// Author: Michael
/// Represents a command to import student records from a CSV file.
/// Use this command to initiate the import of student data from a provided CSV file stream. The command
/// is typically handled by an application service that processes the CSV and creates or updates student records
/// accordingly.</summary>

public record ImportStudentsFromCsvCommand : IRequest<Unit>, ITransactionalCommand
{
    public Stream CsvFile { get; init; } = null!;
}