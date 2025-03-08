using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

//wonder where our dbcontext is stored in the code 
public class DataContext : DbContext
{
    
    public DbSet<AppUser> Users { get; set; }
    public DataContext(DbContextOptions options) : base(options)
    {   
    }

    protected DataContext()
    {
    }
}
