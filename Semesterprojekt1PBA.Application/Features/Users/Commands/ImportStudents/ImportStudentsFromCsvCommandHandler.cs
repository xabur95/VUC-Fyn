using MediatR;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateUser;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.ImportStudents;
/// <summary>
/// Author: Michael
/// Håndterer import af studerende fra en CSV-fil ved at oprette brugerkonti med Student-rollen.
/// Læser filen linje for linje, springer headeren over, og sender en CreateUserCommand per række via MediatR.
/// CSV-filen skal have kolonner i en fast rækkefølge – forkert formatering kan medføre fejl under import.
/// </summary>

public class ImportStudentsFromCsvCommandHandler : IRequestHandler<ImportStudentsFromCsvCommand, Unit>
{
    private readonly IMediator _mediator;

    public ImportStudentsFromCsvCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Unit> Handle(ImportStudentsFromCsvCommand request, CancellationToken cancellationToken)
    {
        var reader = new StreamReader(request.CsvFile);

        await reader.ReadLineAsync();

        var line = await reader.ReadLineAsync();

        while (line != null)
        {
            var columns = line.Split(';');

            await _mediator.Send(new CreateUserCommand
            {
                FirstName = columns[2],
                LastName = columns[3],
                Email = columns[9],
                RoleType = RoleType.Student
            });

            line = await reader.ReadLineAsync();
        }

        return Unit.Value;
    }
}