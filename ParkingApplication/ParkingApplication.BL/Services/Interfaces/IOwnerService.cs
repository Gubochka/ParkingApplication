using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface IOwnerService
{
    Task<Owner> AddOwner(OwnerModel owner);
    Task<OwnerModel>? GetOwnerById(int id);
    Task DeleteOwner(int id);
}