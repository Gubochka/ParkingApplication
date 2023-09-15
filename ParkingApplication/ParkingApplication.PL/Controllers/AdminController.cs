using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DTOs;

namespace ParkingApplication.Controllers;

[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public AdminController(IMapper mapper, IAdminService adminService)
    {
        _mapper = mapper;
        _adminService = adminService;
    }

    [HttpPost("addNewAdmin"), Authorize]
    public async Task<IActionResult> AddNewAdmin([FromBody]AdminDto admin)
    {
        var mapped = _mapper.Map<AdminModel>(admin);
        await _adminService.AddAdmin(mapped);
        
        return Ok();
    }

    [HttpPost("getAllAdmins"), Authorize] 
    public IActionResult AddNewAdmin()
    {
        return Ok(Results.Json(_adminService.GetAllAdmins()));
    }
    
    [HttpDelete("deleteAdmin"), Authorize]
    public async Task<IActionResult> DeleteParking([FromBody]int id)
    {
        await _adminService.DeleteAdmin(id);
        return Ok();
    }
    
    [HttpPut("addParkingToAdmin"), Authorize]
    public async Task<IActionResult> AddParkingToAdmin([FromBody] AdminDto admin)
    {
        var mapped = _mapper.Map<AdminModel>(admin);
        await _adminService.AddParkingToAdmin(mapped);
        return Ok();
    }

    [HttpGet("getCurrentParkingForAdmin"), Authorize]
    public async Task<IActionResult> GetCurrentParkingForAdmin()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var result = await _adminService.GetGetCurrentParkingForAdmin(token);
        return Ok(Results.Json(result));
    }
}