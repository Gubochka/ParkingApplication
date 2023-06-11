using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DTOs;

namespace ParkingApplication.Controllers;

public class ParkingController : Controller
{
    private readonly ILogger<ParkingController> _logger;
    private readonly IParkingTemplateService _parkingTemplateService;
    private readonly IParkingService _parkingService;
    private readonly IMapper _mapper;

    public ParkingController(ILogger<ParkingController> logger, IMapper mapper, IParkingTemplateService parkingTemplateService, IParkingService parkingService)
    {
        _logger = logger;
        _mapper = mapper;
        _parkingTemplateService = parkingTemplateService;
        _parkingService = parkingService;
    }

    [HttpPost("addNewParking"), Authorize]
    public async Task<IActionResult> AddNewParking([FromBody]ParkingTemplateDto parking)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var mapped = _mapper.Map<ParkingTemplateModel>(parking);
        await _parkingTemplateService.AddParkingTemplate(mapped, token);
        
        return Ok();
    }

    [HttpGet("getAllParking"), Authorize]
    public IActionResult GetAllParking()
    {
        var parking = _parkingTemplateService.GetAllParking();
        return Ok(Results.Json(parking));
    }
    
    [HttpDelete("deleteParking"), Authorize]
    public async Task<IActionResult> DeleteParking([FromBody]int id)
    {
        await _parkingTemplateService.DeleteParkingTemplate(id);
        return Ok();
    }
    
    [HttpPost("getParkingSlotsData"), Authorize]
    public async Task<IActionResult> GetParkingSlotsData([FromBody]RequestSlotsDto request)
    {
        var result = _parkingService.GetParkingSlotsData(request.ParkingId, request.Floor);
        return Ok(Results.Json(result));
    }
}