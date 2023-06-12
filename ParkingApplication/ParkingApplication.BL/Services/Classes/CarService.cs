using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.BL.Services.Classes;

public class CarService : ICarService
{
    private readonly ICarRepository _repository;
    private readonly IMapper _mapper;

    public CarService(IMapper mapper, ICarRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<CarModel> AddCar(CarModel car)
    {
        var entity = _mapper.Map<Car>(car);
        var result = _repository.GetAll()
            .FirstOrDefault(x => x.CarName == entity.CarName && x.CarNumber == entity.CarNumber) ??
                     await _repository.AddAsync(entity);
        return _mapper.Map<CarModel>(result);
    }
}