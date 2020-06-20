using CommonLayer;
using CommonLayer.ParkingModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.DBContext
{
    public class ParkingLotDbContext : DbContext
    {
        public ParkingLotDbContext(DbContextOptions<ParkingLotDbContext> options) : base(options)
        {

        }

        public DbSet<UserDetails> UserDetails { get; set; }

        public DbSet<ParkingLotDetails> ParkingLotDetails { get; set; }

        public DbSet<VehicalUnpark> VehicleUnpark { get; set; }
    }
}
