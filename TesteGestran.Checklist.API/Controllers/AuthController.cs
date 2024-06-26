using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteGestran.Checklist.API.Controllers;
using TesteGestran.Checklist.Domain.Auth;
using TesteGestran.Checklist.Domain.Interfaces.Service;
using TesteGestran.Checklist.Services.Utils;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthController(IAuthService authService, JwtTokenGenerator jwtTokenGenerator)
    {
        _authService = authService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginCommand loginModel)
    {
        var user = _authService.IsValidUser(loginModel.Username, loginModel.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "Usuário ou senha inválidos." });
        }

        var token = _jwtTokenGenerator.GenerateJwtToken(user.Username, user.Role);
        return Response(new { token });
    }
}
