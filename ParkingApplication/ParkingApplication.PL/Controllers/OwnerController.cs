using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DTOs;

namespace ParkingApplication.Controllers;

[ApiController]
public class OwnerController : ControllerBase
{
    private readonly IOwnerService _ownerService;
    private readonly IMapper _mapper;

    public OwnerController(IMapper mapper, IOwnerService ownerService)
    {
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