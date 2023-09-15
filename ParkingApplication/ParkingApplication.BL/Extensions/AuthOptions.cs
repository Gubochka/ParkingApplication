using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ParkingApplication.BL.Extensions;

public static class AuthOptions
{
    public static readonly string Issuer = "ParkingApplicationServer";
    public static readonly string Audience = "ParkingApplication";
    private static readonly string Key = Convert.ToBase64String(new HMACSHA256().Key);
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}