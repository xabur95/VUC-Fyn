using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateStudent;
using Semesterprojekt1PBA.Domain.Helpers;
using System.Globalization;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.ImportStudents;
/// <summary>
/// Author: Michael
/// Handles importing students from a CSV file by dispatching a CreateStudentCommand
/// per row via MediatR. Skips the header line. Expects columns in a fixed order.
/// </summary>
public class ImportStudentsFromCsvCommandHandler : IRequestHandler<ImportStudentsFromCsvCommand, Unit>
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public ImportStudentsFromCsvCommandHandler(IMediator mediator, ILogger logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(ImportStudentsFromCsvCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var reader = new StreamReader(request.CsvFile);

            await reader.ReadLineAsync();

            var line = await reader.ReadLineAsync();

            while (line != null)
            {
                var columns = line.Split(';');

                await _mediator.Send(new CreateStudentCommand
                {
                    FirstName = columns[2],
                    LastName = columns[3],
                    Email = columns[9],
                    Knr = columns[0],
                    Tilmeldt = DateOnly.ParseExact(columns[4], "dd.MM.yy", CultureInfo.InvariantCulture),
                    Ophørt = null
                });

                line = await reader.ReadLineAsync();
            }

            return Unit.Value;
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "Error occurred while importing users. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}",
                ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while importing users.");
            throw;
        }
    }
}