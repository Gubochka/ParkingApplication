using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface IOwnerService
{
    Task<OwnerModel> AddOwner(OwnerModel owner);
    List<OwnerModel> GetAllOwners();
}