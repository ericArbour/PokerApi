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
        Dictionary<string, Table> GetTables();
        PublicTable GetTable(string tableId);
        void AddPlayerToTable(string playerConnectionId, string tableName);
        PublicTable StartGame(string tableId);
    }

    public class TableHandler : ITableHandler
    {
        private HashSet<string> _connectedIds = new HashSet<string> { };
        private Dictionary<string, TotalTable> _tables = new Dictionary<string, TotalTable> { };
        private readonly IHubContext<PokerHub> _hubContext;

        public TableHandler(IHubContext<PokerHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void CreateTable(string tableId, string tableName)
        {
            var newTable = new TotalTable { Id = tableId, Name = tableName, Players = new List<Player> { }, isPlaying = false };
            _tables.Add(tableId, newTable);
        }

        public Dictionary<string, Table> GetTables()
        {
            var publicTables = new Dictionary<string, Table> { };
            foreach(KeyValuePair<string, TotalTable> table in _tables)
            {
                publicTables.Add(table.Key, new Table() { Id = table.Value.Id, Name = table.Value.Name, Players = table.Value.Players, isPlaying = table.Value.isPlaying });
            }
            return publicTables;
        }

        public PublicTable GetTable(string tableId)
        {
            var table = _tables[tableId];
            return new PublicTable() { Id = table.Id, Name = table.Name, Players = table.Players, isPlaying = table.isPlaying };
        }

        public void AddPlayerToTable(string playerConnectionId, string tableId)
        {
            _tables[tableId].Players.Add(new Player { Id = playerConnectionId });
        }

        public PublicTable StartGame(string tableId)
        {
            var deck = new Deck();
            var cardValues = new CardValues();
            var handData = new HandData(cardValues, new HandValues(cardValues));
            var game = new Game(handData, deck);
            var table = _tables[tableId];
            table.Game = game;
            table.isPlaying = true;
            return new PublicTable() { Id = table.Id, Name = table.Name, Players = table.Players, isPlaying = table.isPlaying, PublicGameState = table.Game.GetPublicState() };
        }
    }
}
