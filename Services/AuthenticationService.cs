using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using App.Models;
using App.Repositories;
using App.Utils;
using Microsoft.IdentityModel.Tokens;

namespace App.Services;

public class AuthenticationService
{
    private readonly IConfiguration _config;
    private readonly UserRepository _userRepository;
    private readonly AuthenticationRepository _authenticationRepository;

    public AuthenticationService(IConfiguration config, UserRepository userRepository, AuthenticationRepository authenticationRepository)
    {
        _config = config;
        _userRepository = userRepository;
        _authenticationRepository = authenticationRepository;
    }

    public async Task<string> Login(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        if (!CryptoUtils.VerifyPassword(password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid Password");
        }

        // record the authentication
        await _authenticationRepository.CreateAuthenticationAsync(new Authentication()
        {
            User = user,
            Date = DateTime.Now   
        });

        return this.GenerateJWTToken(user);
    }

    public async Task<User> SignUp(User user)
    {
        if (
            await _userRepository.GetUserByUsernameAsync(user.Username) != null
        ) throw new BadHttpRequestException("Username already used.");
        
        user.Password = CryptoUtils.sha256(user.Password);
        var final = await _userRepository.CreateUserAsync(user);
        return final;
    }

    private string GenerateJWTToken(User user) {
        var jwtKey = _config["Jwt:Key"];

        if (jwtKey == null) throw new Exception("Jwt:Key is null");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var Sectoken = new JwtSecurityToken(
            issuer:_config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims: new List<Claim>() {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Username)
            },
            expires: DateTime.Now.AddDays(14), // integrate refresh token
            signingCredentials: credentials);

        var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

        return token;
    }
}