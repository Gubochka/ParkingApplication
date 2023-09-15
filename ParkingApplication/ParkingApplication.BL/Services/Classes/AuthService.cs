using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ParkingApplication.BL.Extensions;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.BL.Services.Classes;

public class AuthService : IAuthService
{
    private readonly IAdminService _adminService;
    private readonly IPasswordHashRepository _passwordHashRepository;
    private readonly IMapper _mapper;

    public AuthService(IAdminService adminService, IMapper mapper, IPasswordHashRepository passwordHashRepository)
    {
        _adminService = adminService;
        _mapper = mapper;
        _passwordHashRepository = passwordHashRepository;
    }
    
    public string GenerateToken(AdminModel admin)
    {
        var token = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: new List<Claim>
            {
                new Claim("adminId", admin.Id.ToString()),
                new Claim("admin", admin.Email),
                new Claim(JwtRegisteredClaimNames.Sub, admin.Email),
            },
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(120)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
            
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task<string?> Login(AdminModel admin)
    {
        var resultAdmin = await _adminService.GetAdminByEmail(admin.Email);
        if (resultAdmin is null) return "EMAIL";
        
        var result = _mapper.Map<Admin>(resultAdmin);
        return _passwordHashRepository.VerifyPassword(admin.Password, result.Password) ? GenerateToken(admin) : "PASSWORD";
    }
}