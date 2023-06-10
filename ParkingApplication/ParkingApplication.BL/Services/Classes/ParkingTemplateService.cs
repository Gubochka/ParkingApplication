using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.BL.Services.Classes;

public class ParkingTemplateService : IParkingTemplateService
{
    private readonly IParkingTemplateRepository _repository;
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public ParkingTemplateService(IMapper mapper, IParkingTemplateRepository repository, IAdminService adminService)
    {
        _mapper = mapper;
        _repository = repository;
        _adminService = adminService;
    }

    public async Task AddParkingTemplate(ParkingTemplateModel parkingTemplate, string token)
    {
        var entity = _mapper.Map<ParkingTemplate>(parkingTemplate);
        await _repository.AddAsync(entity);
    }

    public async Task<ParkingTemplateModel>? GetParkingTemplateById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<ParkingTemplateModel>(entity);
    }

    public async Task DeleteParkingTemplate(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public List<ParkingTemplate> GetAllParking()
    {
        var parking = _repository.GetAll();
        return parking.ToList();
    }
}