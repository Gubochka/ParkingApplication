using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.BL.Services.Classes;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;
    private readonly IParkingTemplateRepository _parkingTemplateRepository;
    private readonly IPasswordHashRepository _passwordHashRepository;
    private readonly IMapper _mapper;

    public AdminService(IMapper mapper, IAdminRepository repository, IParkingTemplateRepository parkingTemplateRepository, IPasswordHashRepository passwordHashRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _parkingTemplateRepository = parkingTemplateRepository;
        _passwordHashRepository = passwordHashRepository;
    }

    public async Task<Admin> AddAdmin(AdminModel admin)
    {
        admin.Password = _passwordHashRepository.HashPassword(admin.Password);
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
    
    public List<Admin> GetAllAdmins()
    {
        return _repository.GetAll().Where(x => x.IsSuperAdmin == false).ToList();
    }
    
    public async Task<bool> CheckAdmin(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var adminEmail = jwtSecurityToken.Claims.First(claim => claim.Type == "sub").Value;

        var admin = await GetAdminByEmail(adminEmail);
        return admin is not null && _mapper.Map<Admin>(admin).IsSuperAdmin;
    }

    public async Task AddParkingToAdmin(AdminModel admin)
    {
        var entity = _mapper.Map<Admin>(admin);
        await _repository.UpdateAsync(entity);
    }

    public async Task<ParkingTemplateModel?> GetGetCurrentParkingForAdmin(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var adminEmail = jwtSecurityToken.Claims.First(claim => claim.Type == "sub").Value;
        var admin = await GetAdminByEmail(adminEmail);
        return admin is not null ?
            _mapper.Map<ParkingTemplateModel>(_parkingTemplateRepository.GetAll().FirstOrDefault(x => admin.ParkingTemplateId == x.Id))
            : null;
    }
}