﻿using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Interfaces;

public interface IAdminService
{
    Task<Admin> AddAdmin(AdminModel admin);
    Task<AdminModel>? GetAdminById(int id);
    Task DeleteAdmin(int id);
}