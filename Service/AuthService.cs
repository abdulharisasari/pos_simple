using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using pos_simple.Config;
using pos_simple.Data;
using pos_simple.DTO;
using pos_simple.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly JWTSettings _jwtSettings;

    public AuthService(AppDbContext db, IOptions<JWTSettings> jwtSettings)
    {
        _db = db;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<User?> Register(string username, string password, int role)
    {
        if (await _db.Users.AnyAsync(u => u.Username == username))
            return null;

        string roleString = role switch
        {
            0 => "Admin",
            1 => "User",
            _ => "User" 
        };

        var user = new User
        {
            Username = username,
            Password = password,
            Role = roleString
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }



    public async Task<LoginResponse?> Login(string username, string password)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        if (user == null)
            return null;

        if (user.Password != password)
            return new LoginResponse
            {
                Code = 401,
                Message = "Incorrect password",
                Data = user,
                Token = null!
            };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new LoginResponse
        {
            Code = 200,
            Message = "Login successful",
            Data = user,
            Token = tokenHandler.WriteToken(token)
        };
    }


}
