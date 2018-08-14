using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PokerApi.Hubs;

namespace PokerApi.Models
{
    public interface IGameHandler
    {
        void CreateTable(string tableConnectionId, string tableName);
        Dictionary<string, Table> GetTables();
        Table GetTable(string tableName);
        void AddPlayerToTable(string playerConnectionId, string tableName);
    }

    public class GameHandler : IGameHandler
    {
        private HashSet<string> _connectedIds = new HashSet<string> { };
        private Dictionary<string, Table> _tables = new Dictionary<string, Table> { };
        private readonly IHubContext<PokerHub> _hubContext;

        public GameHandler(IHubContext<PokerHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void CreateTable(string tableConnectionId, string tableName)
        {
            var newTable = new Table { TableId = tableConnectionId, Players = new List<Player> { } };
            _tables.Add(tableName, newTable);

        }

        public Dictionary<string, Table> GetTables()
        {
            return _tables;
        }

        public Table GetTable(string tableName)
        {
            return _tables[tableName];
        }

        public void AddPlayerToTable(string playerConnectionId, string tableName)
        {
            _tables[tableName].Players.Add(new Player { PlayerId = playerConnectionId });
        }
    }
}
