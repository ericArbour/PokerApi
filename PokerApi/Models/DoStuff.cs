using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }
        public async void AddShowdown()
        {
            var showdown = new Showdown { Deck = "[\"2H\", \"3H\", \"4H\", \"5H\", \"6H\", \"7H\", \"9H\", \"TH\", \"JH\", \"QH\", \"KH\", \"AH\", \"2D\", \"3D\", \"5D\", \"6D\", \"7D\", \"8D\", \"9D\", \"TD\", \"KD\", \"AD\", \"2S\", \"3S\", \"4S\", \"5S\", \"7S\", \"8S\", \"9S\", \"JS\", \"QS\", \"KS\", \"3C\", \"4C\", \"5C\", \"6C\", \"7C\", \"8C\", \"9C\", \"TC\", \"KC\", \"AC\"]", Hand1 = "[\"QD\", \"8H\", \"TS\", \"JC\", \"4D\"]", Hand1Type = "High Card", Hand2 = "[\"2C\", \"JD\", \"6S\", \"QC\", \"AS\"]", Hand2Type = "High Card", Result = "LT" };

            var optionsBuilder = new DbContextOptionsBuilder<ShowdownContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            using (var context = new ShowdownContext(optionsBuilder.Options))
            {
                var hand1 = new List<string> { "QD", "QH", "QS", "QC", "4D" };
                var hand2 = new List<string> { "2C", "2D", "JS", "JC", "AS" };
                var hands = new List<List<string>> { hand1, hand2 };
                var compareHands = new CompareHands();
                var test = compareHands.AssignValuesAndTypes(hands);
                var showdownsController = new ShowdownsController(context);
                await showdownsController.PostShowdown(showdown);
            }
        }
    }
}
