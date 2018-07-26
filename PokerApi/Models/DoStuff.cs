using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PokerApi.Controllers;

namespace PokerApi.Models
{
    public class DoStuff
    {
        public IConfiguration Configuration { get; }
        public DoStuff(IConfiguration configuration)
        {
            Configuration = configuration;
            Timer timer = new Timer(1000);
            timer.Elapsed += async (sender, e) => await HandleTimer();
            timer.Start();
        }
        public async void AddShowdown()
        {
            var deck = new List<string> { "2H", "3H", "4H", "5H", "6H", "7H", "8H", "9H", "TH", "JH", "QH", "KH", "AH", "2D", "3D", "4D", "5D", "6D", "7D", "8D", "9D", "TD", "JD", "QD", "KD", "AD", "2S", "3S", "4S", "5S", "6S", "7S", "8S", "9S", "TS", "JS", "QS", "KS", "AS", "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "TC", "JC", "QC", "KC", "AC" };
            var optionsBuilder = new DbContextOptionsBuilder<ShowdownContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            using (var context = new ShowdownContext(optionsBuilder.Options))
            {
                var cardValues = new CardValues();
                var handData = new HandData(cardValues, new HandValues(cardValues));
                var showdown = new Game(handData, deck).Play();
                var showdownsController = new ShowdownsController(context);
                await showdownsController.PostShowdown(showdown);
            }
        }
        private static Task HandleTimer()
        {
            Console.WriteLine("\nHandler not implemented...");
            throw new NotImplementedException();
        }
    }
}
