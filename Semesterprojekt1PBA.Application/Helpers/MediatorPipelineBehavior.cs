using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;

namespace Semesterprojekt1PBA.Application.Helpers;

public class MediatorPipelineBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly Type _commandMarkerInterface = typeof(ITransactionalCommand);

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var isTransactionalCommand = _commandMarkerInterface.IsAssignableFrom(typeof(TRequest));
        var beginTransactionHasRun = false;

        try
        {
            if (isTransactionalCommand)
            {
                await unitOfWork.BeginTransactionAsync();
                beginTransactionHasRun = true;
            }

            var response = await next();

            if (isTransactionalCommand)
            {
                await unitOfWork.SaveChangesAsync();
            }

            if (isTransactionalCommand)
                await unitOfWork.CommitAsync();

            return response;
        }
        catch (Exception)
        {
            if (isTransactionalCommand && beginTransactionHasRun)
                await unitOfWork.RollbackAsync();

            throw;
        }
    }
}