using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface IParkingService
{
    Task<Parking> AddCarToParking(ParkingModel parking);
    Task<int>? FindCarOnParking(int floor, int slot);
    IQueryable<ParkingModel>? GetAllCarsOnFloor(int floor);
    Task DeleteCarFromParking(int carId, int floor, int slot);
    List<Parking> GetParkingSlotsData(int parkingId, int floor);
}