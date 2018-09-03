using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class Table
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        public bool isPlaying { get; set; }  
    }

    public class TotalTable : Table
    {
        public Game Game;
    }

    public class PublicTable : Table
    {
        public PublicGameState PublicGameState;
    }

    public class Player
    {
        public string Id { get; set; }
        public int Chipcount { get; set; }
        public List<string> HoleCards { get; set; }
    }

    public class PublicGameState
    {
        public List<string> SharedCards { get; set; }
        public int Pot { get; set; }
        public string DealerPlayer { get; set; }
        public string CurrentPlayer { get; set; }
    }
}
