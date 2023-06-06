using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.BL.Services.Classes;

public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _repository;
    private readonly IMapper _mapper;

    public OwnerService(IMapper mapper, IOwnerRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Owner> AddOwner(OwnerModel owner)
    {
        var entity = _mapper.Map<Owner>(owner);
        return await _repository.AddAsync(entity);
    }

    public async Task<OwnerModel>? GetOwnerById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<OwnerModel>(entity);
    }

    public async Task DeleteOwner(int id)
    {
        await _repository.DeleteAsync(id);
    }
}