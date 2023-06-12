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
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public ParkingService(IMapper mapper, IParkingRepository repository, ICarRepository carRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _carRepository = carRepository;
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

    public List<Parking> GetParkingSlotsData(int parkingId, int floor)
    {
        DateTime currentDateTime = DateTime.Now;
        return _repository.GetAll().Where(x => x.FloorNumber == floor 
                                               && x.ParkingTemplateId == parkingId
                                               && x.StandsUntil > currentDateTime).ToList();
    }

    public async Task<List<ReservationDataModel>> GetHistoryForFloor(int parkingId, int floor)
    {
        var parkingHistory = _repository.GetAll().Where(x => x.ParkingTemplateId == parkingId && x.FloorNumber == floor).ToList();
        List<ReservationDataModel> history = new List<ReservationDataModel>();
        foreach (var parking in parkingHistory)
        {
            var data = new ReservationDataModel
            {
                ParkingData = _mapper.Map<ParkingModel>(parking),
                CarData = _mapper.Map<CarModel>(await _carRepository.GetByIdAsync(parking.CarId))
            };
            history.Add(data);
        }

        return history;
    }
}