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
        void CreateGroup(string tableConnectionId, string groupName);
        Dictionary<string, Group> GetGroups();
        void AddConnectedId(string connectedId);
        void RemoveConnectedId(string connectedId);
    }

    public class GameHandler : IGameHandler
    {
        private HashSet<string> _connectedIds = new HashSet<string> { };
        private Dictionary<string, Group> _groups = new Dictionary<string, Group> { };
        private GameState _state = new GameState { Playing = false };
        private readonly IHubContext<PokerHub> _hubContext;

        public GameHandler(IHubContext<PokerHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void CreateGroup(string tableConnectionId, string groupName)
        {
            var newGroup = new Group { TableId = tableConnectionId, PlayerIds = new List<string> { } };
            _groups.Add(groupName, newGroup);

        }

        public Dictionary<string, Group> GetGroups()
        {
            return _groups;
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
