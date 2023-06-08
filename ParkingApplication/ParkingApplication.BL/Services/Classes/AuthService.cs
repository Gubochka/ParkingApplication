using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ParkingApplication.BL.Extensions;
using ParkingApplication.BL.Models;
using ParkingApplication.BL.Services.Interfaces;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL.Services.Classes;

public class AuthService : IAuthService
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public AuthService(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }
    
    public string GenerateToken(AdminModel admin)
    {
        var token = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Email),
                new Claim(JwtRegisteredClaimNames.Sub, admin.Email),
            },
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(120)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
            
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task<string?> Login(AdminModel admin)
    {
        var admins = await _adminService.GetAdminByEmail(admin.Email);
        var result = _mapper.Map<Admin>(admins);
        return result.Password == admin.Password ? GenerateToken(admin) : null;
    }
}