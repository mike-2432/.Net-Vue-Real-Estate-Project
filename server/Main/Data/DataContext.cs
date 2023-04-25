using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Main.Models;

namespace server.Main.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    // Database Tables
    public DbSet<House> Houses => Set<House>();
    public DbSet<User> Users => Set<User>();
  }
}