using System;

//Logical naming structure can name classes in differnt naming sturture 
namespace API.Entities;

public class AppUser
{
    //Like the prop quick shorcut gives you getters and setters  
    public int Id { get; set; }

    //C11 has required
    //Based on API Nullable feild 
    public required string Username { get; set; }


    public required byte[] PasswordHash {get;set;}

    public required byte[] PasswordSalt {get;set;}

}
