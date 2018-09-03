using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class Game
    {
        private HandData _handData { get; set; }
        private List<string> _deck { get; set; }
        private List<string> _hand1 { get; set; }
        private List<string> _hand2 { get; set; }
        private int _currentHand { get; set; }
        private Dictionary<string, string> _playerCards { get; set; }
        public List<string> _sharedCards { get; set; }
        public int _pot { get; set; }
        public string _dealerPlayer { get; set; }
        public string _currentPlayer { get; set; }

        public Game(HandData handData, Deck deck)
        {
            _handData = handData;
            _deck = deck.Shuffle();
            _hand1 = new List<string> { };
            _hand2 = new List<string> { };
            _currentHand = 1;
        }

        public PublicGameState GetPublicState()
        {
            return new PublicGameState() { SharedCards = _sharedCards, Pot = _pot, DealerPlayer = _dealerPlayer, CurrentPlayer = _currentPlayer };
        }

        public Showdown Play()
        {
            while (_hand1.Count < 5 || _hand2.Count() < 5)
            {
                var rnd = new Random().Next(_deck.Count());
                var drawnCard = _deck[rnd];
                if (_currentHand == 1)
                {
                    _hand1.Add(drawnCard);
                    _deck.Remove(drawnCard);
                    _currentHand = 2;
                }
                else if (_currentHand == 2)
                {
                    _hand2.Add(drawnCard);
                    _deck.Remove(drawnCard);
                    _currentHand = 1;
                }
            }

            var hand1Result = _handData.Calculate(_hand1);
            var hand2Result = _handData.Calculate(_hand2);
            var compare = hand1Result.Value > hand2Result.Value ? "GT" : hand2Result.Value > hand1Result.Value ? "LT" : "EQ";
            return new Showdown { Deck = string.Join(",", _deck), Hand1 = string.Join(",", _hand1), Hand1Type = hand1Result.Type, Hand2 = string.Join(",", _hand2), Hand2Type = hand2Result.Type, Result = compare };
        }
    }
}
