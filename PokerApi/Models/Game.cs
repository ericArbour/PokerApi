using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class Game
    {
        private HandCalculator _handCalculator { get; set; }
        private List<string> _deck { get; set; }
        public List<Player> Players { get; set; }
        private Dictionary<string, List<string>> _playerHands = new Dictionary<string, List<string>> { };
        public string CurrentPlayer { get; set; }
        public int Pot { get; set; }

        public Game(HandCalculator handCalculator, Deck deck, List<Player> initialPlayers)
        {
            _handCalculator = handCalculator;
            _deck = deck.Shuffle();
            Players = initialPlayers;
            _playerHands = Players.ToDictionary(player => player.Id, player => new List<string> { });
            CurrentPlayer = Players[0].Id;
            Pot = 0;
            Deal();
        }

        public void Deal()
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (Player player in Players)
                {
                    if (!_playerHands.ContainsKey(player.Id))
                    {
                        _playerHands.Add(player.Id, new List<string> {});
                    } 
                    var rnd = new Random().Next(_deck.Count());
                    var drawnCard = _deck[rnd];
                    _playerHands[player.Id].Add(drawnCard);
                    _deck.Remove(drawnCard);
                }
            }
        }

        public void PlayerAction(string playerId, string actionType, int betAmount)
        {
            if (actionType == "bet")
            {
                Player currentPlayer = Players.Find(player => player.Id == playerId);
                if (currentPlayer.ChipCount >= betAmount)
                {
                    currentPlayer.ChipCount -= betAmount;
                    Pot += betAmount;
                }
            }
        }

        public PublicGameState GetPublicGameState()
        {
            return new PublicGameState() { Pot = Pot, Players = Players, CurrentPlayer = CurrentPlayer };
        }

        public PlayerGameState GetPlayerGameState(string playerId)
        {
            return new PlayerGameState() { Pot = Pot, Players = Players, CurrentPlayer = CurrentPlayer, Hand = _playerHands[playerId] };
        }

        //public Showdown Play()
        //{
        //    while (_hand1.Count < 5 || _hand2.Count() < 5)
        //    {
        //        var rnd = new Random().Next(_deck.Count());
        //        var drawnCard = _deck[rnd];
        //        if (_currentHand == 1)
        //        {
        //            _hand1.Add(drawnCard);
        //            _deck.Remove(drawnCard);
        //            _currentHand = 2;
        //        }
        //        else if (_currentHand == 2)
        //        {
        //            _hand2.Add(drawnCard);
        //            _deck.Remove(drawnCard);
        //            _currentHand = 1;
        //        }
        //    }

        //    var hand1Result = _handCalculator.Calculate(_hand1);
        //    var hand2Result = _handCalculator.Calculate(_hand2);
        //    var compare = hand1Result.Value > hand2Result.Value ? "GT" : hand2Result.Value > hand1Result.Value ? "LT" : "EQ";
        //    return new Showdown { Deck = string.Join(",", _deck), Hand1 = string.Join(",", _hand1), Hand1Type = hand1Result.Type, Hand2 = string.Join(",", _hand2), Hand2Type = hand2Result.Type, Result = compare };
        //}
    }

    public class PublicGameState
    {
        public int Pot;
        public List<Player> Players;
        public string CurrentPlayer;
    }

    public class PlayerGameState
    {
        public int Pot;
        public List<Player> Players;
        public string CurrentPlayer;
        public List<string> Hand;
    }
}
