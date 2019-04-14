using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PokerApi.Hubs;

namespace PokerApi.Models
{
    public interface ITableHandler
    {
        void CreateTable(string tableId, string tableName);
        Dictionary<string, TableSummary> GetTables();
        TableSummary GetTable(string tableId);
        void AddPlayerToTable(string playerConnectionId, string tableName);
        Game StartGame(string tableId);
    }

    public class TableHandler : ITableHandler
    {
        private HashSet<string> _connectedIds = new HashSet<string> { };
        private Dictionary<string, Table> _tables = new Dictionary<string, Table> { };
        private readonly IHubContext<PokerHub> _hubContext;

        public TableHandler(IHubContext<PokerHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void CreateTable(string tableId, string tableName)
        {
            var newTable = new Table { Id = tableId, Name = tableName, Players = new List<Player> { }, isPlaying = false };
            _tables.Add(tableId, newTable);
        }

        public Dictionary<string, TableSummary> GetTables()
        {
            var publicTables = new Dictionary<string, TableSummary> { };
            foreach(KeyValuePair<string, Table> table in _tables)
            {
                publicTables.Add(table.Key, new TableSummary() { Id = table.Value.Id, Name = table.Value.Name, PlayerCount = table.Value.Players.Count(), isPlaying = table.Value.isPlaying });
            }
            return publicTables;
        }

        public TableSummary GetTable(string tableId)
        {
            var table = _tables[tableId];
            return new TableSummary() { Id = table.Id, Name = table.Name, PlayerCount = table.Players.Count(), isPlaying = table.isPlaying };
        }

        public void AddPlayerToTable(string playerConnectionId, string tableId)
        {
            _tables[tableId].Players.Add(new Player { Id = playerConnectionId });
        }

        public Game StartGame(string tableId)
        {
            var deck = new Deck();
            var cardValues = new CardValues();
            var handCalculator = new HandCalculator(cardValues, new HandValues(cardValues));
            var table = _tables[tableId];
            var game = new Game(handCalculator, deck, table.Players);
            table.Game = game;
            table.isPlaying = true;
            return table.Game;
        }
    }
}
