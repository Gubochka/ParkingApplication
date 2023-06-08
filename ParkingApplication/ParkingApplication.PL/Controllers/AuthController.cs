using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DTOs;

namespace ParkingApplication.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(ILogger<AuthController> logger, IAuthService authService, IMapper mapper)
    {
        _logger = logger;
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]AdminDto admin)
    {
        var mappedUser = _mapper.Map<AdminModel>(admin);
        var result = await _authService.Login(mappedUser);
        return Ok(Results.Json(new
        {
            bearer = result
        }));
    }

}