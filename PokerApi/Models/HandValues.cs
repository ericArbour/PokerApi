using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class HandValues
    {
        private CardValues _cardValues { get; set; }

        public HandValues(CardValues cardValues)
        {
            _cardValues = cardValues;
        }

        public int HighCardHandValue(List<string> faces)
        {
            return faces.Aggregate(0, (total, next) => _cardValues.GetFiveKVal(next) + total);
        }

        public int OnePairValue(List<(string, int)> faceCounts)
        {
            return faceCounts.Aggregate(0,
                (total, next) =>
                    next.Item2 == 2 ?
                        (_cardValues.GetCardIndexes(next.Item1) * 2210) + 2427 + total
                        : _cardValues.GetFiveKVal(next.Item1) + total
            );
        }

        public int TwoPairValue(List<(string, int)> faceCounts)
        {
            return faceCounts.Aggregate((0, 0),
                (total, next) =>
                    next.Item2 == 2 ?
                        total.Item2 == 0 ?
                            (((_cardValues.GetOneKVal(next.Item1) - 2) * (_cardValues.GetOneKVal(next.Item1) - 1) * 13) + 31157 + total.Item1, 1) :
                            ((_cardValues.GetOneKVal(next.Item1) * 13) + total.Item1, total.Item2)
                        : (_cardValues.GetOneKVal(next.Item1) + total.Item1, total.Item2)
             ).Item1;
        }

        public int SetValue(List<(string, int)> faceCounts)
        {
            return faceCounts.Aggregate(0,
                (total, next) =>
                    next.Item2 == 3 ?
                        (_cardValues.GetCardIndexes(next.Item1) * 608) + 33042 + total :
                        _cardValues.GetTwoKVal(next.Item1) + total
            );
        }

        public int FullHouseValue(List<(string, int)> faceCounts)
        {
            return faceCounts.Aggregate(0,
                (total, next) =>
                    next.Item2 == 3 ?
                        (_cardValues.GetCardIndexes(next.Item1) * 13) + 43387 + total :
                    next.Item2 == 2 ?
                        _cardValues.GetOneKVal(next.Item1) + total :
                        total
            );
        }

        public int QuadValue(List<(string, int)> faceCounts)
        {
            return faceCounts.Aggregate(0,
                (total, next) =>
                    next.Item2 == 4 ?
                        (_cardValues.GetCardIndexes(next.Item1) * 13) + 43556 + total :
                        _cardValues.GetOneKVal(next.Item1) + total
            );
        }

        public int StraightValue(string highCard)
        {
            return _cardValues.GetCardIndexes(highCard) + 40948;
        }
    }
}
