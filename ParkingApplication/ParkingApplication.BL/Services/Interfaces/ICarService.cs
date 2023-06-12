using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface ICarService
{
    Task<CarModel> AddCar(CarModel car);
}