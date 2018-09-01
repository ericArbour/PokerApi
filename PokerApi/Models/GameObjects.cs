using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class Table
    {
        public string TableId { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
    }

    public class Player
    {
        public string PlayerId { get; set; }
        public int Chipcount { get; set; }
        public List<string> HoleCards { get; set; }
    }
}
