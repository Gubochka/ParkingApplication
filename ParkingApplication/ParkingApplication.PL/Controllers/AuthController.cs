using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DTOs;

namespace ParkingApplication.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public AuthController(ILogger<AuthController> logger, IAuthService authService, IMapper mapper, IAdminService adminService)
    {
        _logger = logger;
        _authService = authService;
        _mapper = mapper;
        _adminService = adminService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]AdminDto admin)
    {
        var mappedUser = _mapper.Map<AdminModel>(admin);
        var result = await _authService.Login(mappedUser);
        return Ok(Results.Json(new { bearer = result }));
    }

    [HttpGet("checkAdmin"), Authorize]
    public async Task<IActionResult> CheckAdmin()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var result = await _adminService.CheckAdmin(token);
        return Ok(Results.Json(new { superAdmin = result }));
    }
}