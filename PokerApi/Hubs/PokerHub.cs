using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PokerApi.Models;

namespace PokerApi.Hubs
{
    public class PokerHub : Hub
    {
        private ITableHandler _tableHandler { get; set; }

        public PokerHub(ITableHandler tableHandler)
        {
            _tableHandler = tableHandler;
        }

        public async Task CreateTable(string tableName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.ConnectionId);
            _tableHandler.CreateTable(Context.ConnectionId, tableName);
            await Clients.Caller.SendAsync("TableCreated", _tableHandler.GetTableSummary(Context.ConnectionId));
            await Clients.All.SendAsync("PostTables", _tableHandler.GetTableSummaries());
        }

        public async Task GetTables()
        {
            await Clients.Caller.SendAsync("PostTables", _tableHandler.GetTableSummaries());
        }

        public async Task JoinTable(string tableId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
            _tableHandler.AddPlayerToTable(Context.ConnectionId, tableId);
            var tableSummary = _tableHandler.GetTableSummary(tableId);
            await Clients.Caller.SendAsync("JoinedTable", tableSummary, Context.ConnectionId);
            await Clients.Client(tableId).SendAsync("TableUpdated", tableSummary);
            foreach (Player player in tableSummary.Players)
            {
                await Clients.Client(player.Id).SendAsync("TableUpdated", tableSummary);
            }
            await Clients.All.SendAsync("PostTables", _tableHandler.GetTableSummaries());
        }

        public async Task StartGame()
        {
            var tableSummary = _tableHandler.StartGame(Context.ConnectionId);
            await Clients.Caller.SendAsync("TableUpdated", tableSummary);
            foreach(Player player in tableSummary.Players)
            {
                await Clients.Client(player.Id).SendAsync("TableUpdated", tableSummary);
            }
        }

        public async Task GetPlayerGameState(string tableId)
        {
            var playerGameState = _tableHandler.GetPlayerGameState(tableId, Context.ConnectionId);
            await Clients.Caller.SendAsync("PostPlayerGameState", playerGameState);
        }

        public async Task GetPublicGameState()
        {
            var publicGameState = _tableHandler.GetPublicGameState(Context.ConnectionId);
            await Clients.Caller.SendAsync("PostPublicGameState", publicGameState);
        }

        public async Task PlayerAction(string tableId, string actionType, int betAmount)
        {
            (TableSummary tableSummary, PublicGameState publicGameState, Dictionary<string, PlayerGameState> playerGameStates) = _tableHandler.PlayerAction(tableId, Context.ConnectionId, actionType, betAmount);
            await Clients.Client(tableId).SendAsync("PostPublicGameState", publicGameState);
            foreach (Player player in tableSummary.Players)
            {
                await Clients.Client(player.Id).SendAsync("PostPlayerGameState", playerGameStates[player.Id]);
            }
        }
    }
}
