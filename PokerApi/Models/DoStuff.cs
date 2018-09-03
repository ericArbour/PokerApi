using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PokerApi.Controllers;

namespace PokerApi.Models
{
    public class DoStuff
    {
        private IConfiguration Configuration { get; }

        public DoStuff(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async void AddShowdown(int numberOfShowdowns = 1000000)
        {
            var deck = new Deck();
            var optionsBuilder = new DbContextOptionsBuilder<ShowdownContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            using (var context = new ShowdownContext(optionsBuilder.Options))
            {
                var cardValues = new CardValues();
                var handData = new HandData(cardValues, new HandValues(cardValues));
                var game = new Game(handData, deck);
                var showdownsController = new ShowdownsController(context);

                for (var i = 0; i < numberOfShowdowns; i++)
                {
                    await showdownsController.PostShowdown(game.Play());
                }
            }
        }
    }
}
