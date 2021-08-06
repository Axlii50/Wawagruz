using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wawagruz.Models;

namespace Wawagruz.Data
{
    public class WawagruzContext : DbContext
    {
        public WawagruzContext (DbContextOptions<WawagruzContext> options)
            : base(options)
        {
        }

        public DbSet<Wawagruz.Models.OrderModel> Order { get; set; }
        public DbSet<Wawagruz.Models.DeliveryModel> Delivery { get; set; }
    }
}
