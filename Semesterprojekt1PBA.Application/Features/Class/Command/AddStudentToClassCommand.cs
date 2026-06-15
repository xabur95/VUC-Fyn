using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Class.Command;

public record AddStudentToClassCommand(Guid ClassId, Guid StudentId)
    : IRequest<Unit>, ITransactionalCommand;

public class AddStudentToClassCommandHandler(
    IClassRepository classRepository,
    ILogger<AddStudentToClassCommandHandler> logger)
    : IRequestHandler<AddStudentToClassCommand, Unit>
{
    public async Task<Unit> Handle(AddStudentToClassCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await classRepository.AddStudentToClassAsync(request.ClassId, request.StudentId);
            return Unit.Value;
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error in AddStudentToClassCommand. ErrorCode: {ErrorCode}", ex.ErrorCode);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error in AddStudentToClassCommand.");
            throw;
        }
    }
}
