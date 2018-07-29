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
            var deck = new List<string> { "2H", "3H", "4H", "5H", "6H", "7H", "8H", "9H", "TH", "JH", "QH", "KH", "AH", "2D", "3D", "4D", "5D", "6D", "7D", "8D", "9D", "TD", "JD", "QD", "KD", "AD", "2S", "3S", "4S", "5S", "6S", "7S", "8S", "9S", "TS", "JS", "QS", "KS", "AS", "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "TC", "JC", "QC", "KC", "AC" };
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
