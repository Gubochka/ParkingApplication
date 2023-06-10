using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.BL.Services.Classes;

public class ParkingService : IParkingService
{
    private readonly IParkingRepository _repository;
    private readonly IMapper _mapper;

    public ParkingService(IMapper mapper, IParkingRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Parking> AddCarToParking(ParkingModel parking)
    {
        var entity = _mapper.Map<Parking>(parking);
        return await _repository.AddAsync(entity);
    }

    public async Task<int>? FindCarOnParking(int floor, int slot)
    {
        var id = await _repository.FindCarOnParking(floor, slot);
        return id;
    }

    public IQueryable<ParkingModel>? GetAllCarsOnFloor(int floor)
    {
        var entities = _repository.GetAllCarsOnFloor(floor);
        return _mapper.ProjectTo<ParkingModel>(entities);
    }

    public async Task DeleteCarFromParking(int carId, int floor, int slot)
    {
        await _repository.DeleteCarFromParking(carId, floor, slot);
    }
}