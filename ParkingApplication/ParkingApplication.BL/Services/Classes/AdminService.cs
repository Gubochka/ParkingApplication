﻿using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.BL.Services.Classes;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;
    private readonly IMapper _mapper;

    public AdminService(IMapper mapper, IAdminRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Admin> AddAdmin(AdminModel admin)
    {
        var entity = _mapper.Map<Admin>(admin);
        return await _repository.AddAsync(entity);
    }

    public async Task<AdminModel?> GetAdminById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<AdminModel>(entity);
    }

    public async Task<AdminModel?> GetAdminByEmail(string email)
    {
        var entity = await _repository.GetAdminByEmail(email);
        return _mapper.Map<AdminModel>(entity);
    }

    public async Task DeleteAdmin(int id)
    {
        await _repository.DeleteAsync(id);
    }
}