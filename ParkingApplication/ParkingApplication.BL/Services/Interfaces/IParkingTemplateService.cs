using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface IParkingTemplateService
{
    Task<ParkingTemplate> AddParkingTemplate(ParkingTemplateModel parkingTemplate);
    Task<ParkingTemplateModel>? GetParkingTemplateById(int id);
    Task DeleteParkingTemplate(int id);
}