using Semesterprojekt1PBA.Application.Dto.Users;

namespace Evaluation.Web.Services;

public class AuthState
{
    public LoginResponse? CurrentUser { get; private set; }
    public string? ActiveRole { get; private set; }
    public bool IsLoggedIn => CurrentUser is not null;

    public void Login(LoginResponse user)
    {
        CurrentUser = user;
        ActiveRole = user.Roles.FirstOrDefault().ToString();
    }

    public void SelectRole(string role)
    {
        ActiveRole = role;
    }

    public void Logout()
    {
        CurrentUser = null;
        ActiveRole = null;
    }
}
