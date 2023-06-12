using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface IParkingService
{
    Task<Parking> AddCarToParking(ParkingModel parking);
    List<Parking> GetParkingSlotsData(int parkingId, int floor);
    Task<ReservationDataModel?> GetSlotData(int parkingId, int floor, int? slot);
    Task<List<ReservationDataModel>> GetHistoryForFloor(int parkingId, int floor);
}