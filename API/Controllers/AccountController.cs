using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{

    //dto data transfer object
    [HttpPost("register")] // accout/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {

        if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");
        //pull all resources 
        using var hmac = new HMACSHA512(); 

        var user = new AppUser {
            Username = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto 
        {
            Username = user.Username,
            token = tokenService.CreateToken(user)
        };

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == loginDto.Username.ToLower());

        if (user == null) return Unauthorized("invalid username");

        using var hamac = new HMACSHA512(user.PasswordSalt);

        var ComputeHash = hamac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < ComputeHash.Length; i++)
        {
            if(ComputeHash[i] != user.PasswordHash[i]){
                return Unauthorized("invalid password");
            }

        }

        return new UserDto
        {
            Username = user.Username,
            token = tokenService.CreateToken(user)
        }; 

    }

    private async Task<bool> UserExists(string Username){
        return await context.Users.AnyAsync(s => s.Username.ToLower() == Username.ToLower());
    }

}
