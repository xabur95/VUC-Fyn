using MediatR;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateUser;
using Semesterprojekt1PBA.Application.Features.Users.Commands.ImportStudents;
using System.Text;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Unittests for ImportStudentsFromCsvCommandHandler. Verificerer at handleren korrekt behandler CSV-data
/// og interagerer med IMediator som forventet.
/// </summary>

public class ImportStudentsFromCsvCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenCsvHasOneStudentRow_SendsCreateUserCommandOnce()
    {
        // Arrange
        var csv = "K.nr.;K.id.;Fornavn;Efternavn;Tilmeldt;Ophørt;Pause;Bemærkning;Telefon;Mail;Adresse;Post/By;F.dato\n100001;;Jens;Hansen;23.08.25;;;;12345678;jens@hansen.dk;Bondegaard 4;9999 Landsby; 190210";
        var mockMediator = new Mock<IMediator>();
        MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(csv));
        var importStudentsCommandHandler = new ImportStudentsFromCsvCommandHandler(mockMediator.Object);

        // Act
        var result = await importStudentsCommandHandler.Handle(new ImportStudentsFromCsvCommand { CsvFile = stream}, CancellationToken.None);

        // Assert
        mockMediator.Verify(r => r.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once());
    }
}