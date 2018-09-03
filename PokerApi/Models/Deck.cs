using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class Deck
    {
        private List<string> _freshDeck = new List<string> { "2H", "3H", "4H", "5H", "6H", "7H", "8H", "9H", "TH", "JH", "QH", "KH", "AH", "2D", "3D", "4D", "5D", "6D", "7D", "8D", "9D", "TD", "JD", "QD", "KD", "AD", "2S", "3S", "4S", "5S", "6S", "7S", "8S", "9S", "TS", "JS", "QS", "KS", "AS", "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "TC", "JC", "QC", "KC", "AC" };
        private List<string> _activeDeck = new List<string> { };

        public List<string> Shuffle()
        {
            _activeDeck = new List<string>(_freshDeck);
            var shuffledDeck = new List<string> { };
            while (_activeDeck.Count > 0)
            {
                var rnd = new Random().Next(_activeDeck.Count());
                var randomCard = _activeDeck[rnd];
                shuffledDeck.Add(randomCard);
                _activeDeck.Remove(randomCard);
            }
            return shuffledDeck;
        }
    }
}
