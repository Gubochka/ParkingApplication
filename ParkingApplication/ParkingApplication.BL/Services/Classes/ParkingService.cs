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
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;

    public ParkingService(IMapper mapper, IParkingRepository repository, ICarRepository carRepository, IOwnerRepository ownerRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _carRepository = carRepository;
        _ownerRepository = ownerRepository;
    }

    public async Task<Parking> AddCarToParking(ParkingModel parking)
    {
        var entity = _mapper.Map<Parking>(parking);
        return await _repository.AddAsync(entity);
    }

    public List<Parking> GetParkingSlotsData(int parkingId, int floor)
    {
        DateTime currentDateTime = DateTime.Now;
        return _repository.GetAll().Where(x => x.FloorNumber == floor 
                                               && x.ParkingTemplateId == parkingId
                                               && x.StandsUntil > currentDateTime).ToList();
    }

    public async Task<ReservationDataModel?> GetSlotData(int parkingId, int floor, int? slot)
    {
        var parking = _repository.GetAll().FirstOrDefault(x => x.ParkingTemplateId == parkingId && x.FloorNumber == floor && x.SlotNumber == slot);
        if (parking is null) return null;

        var car = await _carRepository.GetByIdAsync(parking.CarId);
        var owner = await _ownerRepository.GetByIdAsync(car.OwnerId);
        return new ReservationDataModel
        {
            OwnerData = _mapper.Map<OwnerModel>(owner),
            ParkingData = _mapper.Map<ParkingModel>(parking),
            CarData = _mapper.Map<CarModel>(car)
        };
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