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

    public async Task<OwnerModel> AddOwner(OwnerModel owner)
    {
        var entity = _mapper.Map<Owner>(owner);
        var result = _repository.GetAll().FirstOrDefault(x => x.FullName == entity.FullName && x.PhoneNumber == entity.PhoneNumber) ??
                     await _repository.AddAsync(entity);
        return _mapper.Map<OwnerModel>(result);
    }

    public List<OwnerModel> GetAllOwners()
    {
        var entities = _repository.GetAll().ToList();
        return _mapper.Map<List<OwnerModel>>(entities);
    }
}