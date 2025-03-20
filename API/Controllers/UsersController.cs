using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class UsersController(DataContext context) : BaseApiController
{
//Contoller injection and thats more simillar to my way?? 
[AllowAnonymous]
[HttpGet]
public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
{
    var users = await context.Users.ToListAsync();

    //return type action result 
    return Ok(users); 
}

[Authorize]
[HttpGet("{id}")] // /api/users/
public async Task<ActionResult<AppUser>> GetUser(int id)
{
    var user = await context.Users.FindAsync(id);
    if(user == null){
        return NotFound();
    }
    //return type action result 
    return user;
}



}
