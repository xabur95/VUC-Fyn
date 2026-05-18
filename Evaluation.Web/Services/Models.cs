namespace Evaluation.Web.Services;

public record UserDto(Guid Id, string FirstName, string LastName, string Email);

public record QuestionDto(
    Guid Id,
    string Title,
    string Text,
    int Points,
    string ActiveStatus,
    AnswerDto? Answer);

public record AnswerDto(Guid Id, string Title, string Text);



public record CreateQuestionRequest(
    Guid CreatedByUserId,
    string Title,
    string Text,
    int Points,
    int ActiveStatus,
    Guid? ParentQuestionId,
    List<Guid>? TagIds,
    List<Guid>? SubjectIds);


// Ensure these records include Password:
public record CreateUserRequest(string FirstName, string LastName, string Email, string Password);
public record CreateStudentRequest(string FirstName, string LastName, string Email, string Password, string Knr, DateOnly Tilmeldt, DateOnly? Ophørt);
