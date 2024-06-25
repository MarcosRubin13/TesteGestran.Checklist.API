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
        if (_authService.IsValidUser(loginModel.Username, loginModel.Password))
        {
            var token = _jwtTokenGenerator.GenerateJwtToken(loginModel.Username);
            return Response(new { token });
        }
        return Unauthorized();
    }
}
