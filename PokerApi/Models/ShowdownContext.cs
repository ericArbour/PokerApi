using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PokerApi.Models
{
    public class ShowdownContext : DbContext 
    {
        public ShowdownContext(DbContextOptions<ShowdownContext> options) : base(options)
        {

        }

        public DbSet<Showdown> Showdowns { get; set; }
    }
}
