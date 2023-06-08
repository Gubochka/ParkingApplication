using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ParkingApplication.BL.Extensions;

public static class AuthOptions
{
    public const string ISSUER = "ParkingApplicationServer";
    public const string AUDIENCE = "ParkingApplication";
    private static readonly string KEY = Convert.ToBase64String(new HMACSHA256().Key);
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}