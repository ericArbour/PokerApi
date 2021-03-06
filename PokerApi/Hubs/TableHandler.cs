﻿using System;
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
        List<TableSummary> GetTableSummaries();
        TableSummary GetTableSummary(string tableId);
        void AddPlayerToTable(string playerConnectionId, string tableName);
        TableSummary StartGame(string tableId);
        (TableSummary, PublicGameState, Dictionary<string, PlayerGameState>) PlayerAction(string tableId, string playerId, string actionType, int betAmoun);
        PlayerGameState GetPlayerGameState(string tableId, string playerId);
        PublicGameState GetPublicGameState(string tableId);
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

        public List<TableSummary> GetTableSummaries()
        {
            return _tables.Select(table => table.Value.GetTableSummary()).ToList();
        }

        public TableSummary GetTableSummary(string tableId)
        {
            return _tables[tableId].GetTableSummary();
        }

        public void AddPlayerToTable(string playerConnectionId, string tableId)
        {
            _tables[tableId].Players.Add(new Player { Id = playerConnectionId, ChipCount = 100 });
        }

        public TableSummary StartGame(string tableId)
        {
            var deck = new Deck();
            var cardValues = new CardValues();
            var handCalculator = new HandCalculator(cardValues, new HandValues(cardValues));
            var table = _tables[tableId];
            var game = new Game(handCalculator, deck, table.Players);
            table.Game = game;
            table.isPlaying = true;
            return table.GetTableSummary();
        }

        public (TableSummary, PublicGameState, Dictionary<string, PlayerGameState>) PlayerAction(string tableId, string playerId, string actionType, int betAmount)
        {
            Table table = _tables[tableId];
            table.Game.PlayerAction(playerId, actionType, betAmount);
            var playerGameStates = table.Game.Players.ToDictionary(player => player.Id, player => table.Game.GetPlayerGameState(player.Id));
            return (table.GetTableSummary(), table.Game.GetPublicGameState(), playerGameStates);
        }

        public PlayerGameState GetPlayerGameState(string tableId, string playerId)
        {
            return _tables[tableId].Game.GetPlayerGameState(playerId);
        }

        public PublicGameState GetPublicGameState(string tableId)
        {
            return _tables[tableId].Game.GetPublicGameState();
        }
    }
}
