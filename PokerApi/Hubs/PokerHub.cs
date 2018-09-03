﻿using System;
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
            await Clients.Caller.SendAsync("TableCreated", _tableHandler.GetTable(Context.ConnectionId));
            await Clients.All.SendAsync("ViewTables", _tableHandler.GetTables());
        }

        public async Task GetTables()
        {
            await Clients.Caller.SendAsync("ViewTables", _tableHandler.GetTables());
        }

        public async Task JoinTable(string tableId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
            _tableHandler.AddPlayerToTable(Context.ConnectionId, tableId);
            var table = _tableHandler.GetTable(tableId);
            await Clients.Client(tableId).SendAsync("TableUpdated", table);
            await Clients.Caller.SendAsync("JoinedTable", table.isPlaying);
        }

        public async Task StartGame()
        {
            var table = _tableHandler.StartGame(Context.ConnectionId);
            await Clients.Caller.SendAsync("GameStarted", table);
            await Clients.Others.SendAsync("GameStarted", table.isPlaying);
        }
    }
}
