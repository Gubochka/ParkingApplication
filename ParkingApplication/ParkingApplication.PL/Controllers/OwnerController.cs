using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DTOs;

namespace ParkingApplication.Controllers;

public class OwnerController : Controller
{
    private readonly ILogger<OwnerController> _logger;
    private readonly IOwnerService _ownerService;
    private readonly IMapper _mapper;

    public OwnerController(ILogger<OwnerController> logger, IMapper mapper, IOwnerService ownerService)
    {
        _logger = logger;
        _mapper = mapper;
        _ownerService = ownerService;
    }

    [HttpGet("getAllOwnersNames"), Authorize]
    public IActionResult GtAllOwnersNames()
    {
        var result = _ownerService.GetAllOwners();
        return Ok(Results.Json(_mapper.Map<List<OwnerDto>>(result)));
    }
}