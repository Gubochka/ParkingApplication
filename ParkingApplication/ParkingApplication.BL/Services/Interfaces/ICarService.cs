using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface ICarService
{
    Task<Car> AddCar(CarModel car);
    Task<CarModel>? GetCarById(int id);
    Task DeleteCar(int id);
}