using Agenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Infrastructure.Data
{
    public class AgendaDbContext : DbContext
    {

        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options){ }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Contato>().HasKey(c => c.Id);

        }
    }
}
