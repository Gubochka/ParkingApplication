using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.BL.Services.Classes;

public class ParkingTemplateService : IParkingTemplateService
{
    private readonly IParkingTemplateRepository _repository;
    private readonly IMapper _mapper;

    public ParkingTemplateService(IMapper mapper, IParkingTemplateRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ParkingTemplate> AddParkingTemplate(ParkingTemplateModel parkingTemplate)
    {
        var entity = _mapper.Map<ParkingTemplate>(parkingTemplate);
        return await _repository.AddAsync(entity);
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
}