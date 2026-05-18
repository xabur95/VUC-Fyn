using System.Net.Http.Json;
using Semesterprojekt1PBA.Application.Dto.Users;

namespace Evaluation.Web.Services;

public class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<LoginResponse?> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("/users/login", new { Email = email, Password = password });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<GetUserByIdResponse?> GetUserByIdAsync(Guid id)
    {
        return await _http.GetFromJsonAsync<GetUserByIdResponse>($"/users/{id}");
    }

    public async Task<List<UserDto>?> GetUsersByRoleAsync(string roleType)
    {
        return await _http.GetFromJsonAsync<List<UserDto>>($"/users/role/{roleType}");
    }

    public async Task<List<QuestionDto>?> GetAllQuestionsAsync()
    {
        return await _http.GetFromJsonAsync<List<QuestionDto>>("/questions");
    }

    public async Task CreateTeacherAsync(CreateUserRequest request)
    {
        var response = await _http.PostAsJsonAsync("/users/teacher", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateStudentAsync(CreateStudentRequest request)
    {
        var response = await _http.PostAsJsonAsync("/users/student", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateQuestionAsync(CreateQuestionRequest request)
    {
        var response = await _http.PostAsJsonAsync("/questions", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeactivateUserAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"/users/{id}/deactivate");
        response.EnsureSuccessStatusCode();
    }
}

