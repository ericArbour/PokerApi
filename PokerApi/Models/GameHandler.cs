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
        HashSet<string> GetConnectedIds { get; }
        GameState GetState { get;  }
        void AddConnectedId(string connectedId);
        void RemoveConnectedId(string connectedId);
    }

    public class GameHandler : IGameHandler
    {
        private HashSet<string> _connectedIds = new HashSet<string> { };
        private GameState _state = new GameState { Playing = false };
        private readonly IHubContext<ChatHub> _hubContext;

        public GameHandler(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public HashSet<string> GetConnectedIds
        {
            get
            {
                return _connectedIds;
            }
        }

        public void AddConnectedId(string connectedId)
        {
            _connectedIds.Add(connectedId);
            if (_connectedIds.Count() > 1)
            {
                _hubContext.Clients.All.SendAsync("GameReady");
            }
        }

        public void RemoveConnectedId(string connectedId)
        {
            _connectedIds.Remove(connectedId);
        }

        public GameState GetState
        {
            get
            {
                return _state;
            }
        }

        public void StartGame()
        {
            _state.Playing = true;
        }
    }
}
