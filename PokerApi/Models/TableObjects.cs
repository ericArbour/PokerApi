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
        public Game Game { get; set; }
        public TableSummary GetTableSummary()
        {
            return new TableSummary { Id = Id, Name = Name, Players = Players, isPlaying = isPlaying };
        }
    }

    public class TableSummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        public bool isPlaying { get; set; }
    }

    public class Player
    {
        public string Id { get; set; }
        public int ChipCount { get; set; }
    }
}
