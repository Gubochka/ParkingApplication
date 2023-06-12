using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DTOs;

namespace ParkingApplication.Controllers;

public class ParkingController : Controller
{
    private readonly ILogger<ParkingController> _logger;
    private readonly IParkingTemplateService _parkingTemplateService;
    private readonly IParkingService _parkingService;
    private readonly IOwnerService _ownerService;
    private readonly ICarService _carService;
    private readonly IMapper _mapper;

    public ParkingController(ILogger<ParkingController> logger, IMapper mapper, IParkingTemplateService parkingTemplateService, IParkingService parkingService, IOwnerService ownerService, ICarService carService)
    {
        _logger = logger;
        _mapper = mapper;
        _parkingTemplateService = parkingTemplateService;
        _parkingService = parkingService;
        _ownerService = ownerService;
        _carService = carService;
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
    
    [HttpPost("getPriceByDateTime"), Authorize]
    public async Task<IActionResult> GetPriceByDateTime([FromBody]DateTimePriceDto request)
    {
        var result = _parkingTemplateService.GetPriceByDateTime(request.StandsUntil, request.ParkingId);
        return Ok(Results.Json(new
        {
            price = result
        }));
    }
    
    [HttpPost("reservationCarToParking"), Authorize]
    public async Task<IActionResult> ReservationCarToParking([FromBody]ReservationDataDto data)
    {
        var mappedOwner = _mapper.Map<OwnerModel>(data.OwnerData);
        var ownerResult = await _ownerService.AddOwner(mappedOwner);
        
        var mappedCar = _mapper.Map<CarModel>(data.CarData);
        mappedCar.OwnerId = ownerResult.Id;
        var carResult = await _carService.AddCar(mappedCar);
        
        var mappedParking = _mapper.Map<ParkingModel>(data.ParkingData);
        mappedParking.CarId = carResult.Id;
        var parkingResult = await _parkingService.AddCarToParking(mappedParking);

        return Ok();
    }
}