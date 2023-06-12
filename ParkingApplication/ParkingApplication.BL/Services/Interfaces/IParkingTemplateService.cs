using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface IParkingTemplateService
{
    Task AddParkingTemplate(ParkingTemplateModel parkingTemplate, string token);
    Task<ParkingTemplateModel>? GetParkingTemplateById(int id);
    Task DeleteParkingTemplate(int id);
    List<ParkingTemplate> GetAllParking();
    Task<float> GetPriceByDateTime(DateTime dateTime, int parkingId);
}