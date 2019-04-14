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
        //public async void AddShowdown(int numberOfShowdowns = 1000000)
        //{
        //    var deck = new Deck();
        //    var optionsBuilder = new DbContextOptionsBuilder<ShowdownContext>();
        //    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

        //    using (var context = new ShowdownContext(optionsBuilder.Options))
        //    {
        //        var cardValues = new CardValues();
        //        var handCalculator = new HandCalculator(cardValues, new HandValues(cardValues));
        //        var game = new Game(handCalculator, deck, new List<Player>() { new Player { Id = "playerOne" }, new Player { Id = "playerTwo" } });
        //        var showdownsController = new ShowdownsController(context);

        //        for (var i = 0; i < numberofshowdowns; i++)
        //        {
        //            await showdownscontroller.postshowdown(game);
        //        }
        //    }
        //}
    }
}
