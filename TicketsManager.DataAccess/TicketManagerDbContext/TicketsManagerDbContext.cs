using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketsManager.DataAccess.Entity;

namespace TicketsManager.DataAccess.TicketManagerDbContext
{
    public class TicketsManagerDbContext : DbContext
    {
        public TicketsManagerDbContext(DbContextOptions<TicketsManagerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
        public DbSet<UserEntity> Users { get; set; }
    }
}
