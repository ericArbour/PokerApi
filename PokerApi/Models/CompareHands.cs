using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class CompareHands
    {
        private CardValues _cardValues { get; set; }
        private HandValues _handValues { get; set; }

        public CompareHands(CardValues cardValues, HandValues handValues)
        {
            _cardValues = cardValues;
            _handValues = handValues;
        }

        public List<HandWithData> AssignValuesAndTypes(List<List<string>> hands)
        {
            var handValuesAndTypes = hands.Select(hand => GetValuesAndTypes(hand)).ToList();
            return hands.Select((hand, i) => new HandWithData { Hand = hand, Value = handValuesAndTypes[i].Item1, Type = handValuesAndTypes[i].Item2 }).ToList();
        }

        private (int, string) GetValuesAndTypes(List<string> hand)
        {
            var orderedFaces = hand.Select(card => card.Substring(0, 1)).OrderByDescending(face => _cardValues.GetCardIndexes(face)).ToList();
            var uniqueFaces = orderedFaces.Distinct().ToList();

            (string, int) uniqueFaceToCountTuple(string uniqueFace) =>
                (uniqueFace, orderedFaces.Where(orderedFace => uniqueFace == orderedFace).ToList().Count());

            var faceCounts = uniqueFaces.Select(uniqueFaceToCountTuple).ToList();

            var pairCount = CountMatches(faceCounts, 2);
            var setCount = CountMatches(faceCounts, 3);
            var quadCount = CountMatches(faceCounts, 4);

            var straightHighCard = CheckStraight(uniqueFaces);

            var isFlush = CheckFlush(hand);

            var valueAndType =
                !String.IsNullOrEmpty(straightHighCard) ?
                   isFlush ? (_handValues.StraightValue(straightHighCard) + 43725, "Stright Flush")
                   : (_handValues.StraightValue(straightHighCard), "Straight")
                :
                quadCount == 1 ? (_handValues.QuadValue(faceCounts), "Quad") :
                setCount == 1 && pairCount == 1 ? (_handValues.FullHouseValue(faceCounts), "Full House") :
                isFlush ? (_handValues.HighCardHandValue(orderedFaces) + 40960, "Flush") :
                setCount == 1 ? (_handValues.SetValue(faceCounts), "Set") :
                pairCount == 2 ? (_handValues.TwoPairValue(faceCounts), "Two Pair") :
                pairCount == 1 ? (_handValues.OnePairValue(faceCounts), "One Pair") :
                (_handValues.HighCardHandValue(orderedFaces), "High Card");

            return valueAndType;
        }

        private bool CheckFlush(List<string> cards)
        {
            return cards.Select(card => card.Substring(1, 1)).Distinct().ToList().Count == 1;
        }

        private string CheckStraight(List<string> faces)
        {
            if (faces.Count() == 5)
            {

                var highestCard = faces[0];
                var lowestCard = faces[4];
                var isStraightWithAceHighIndexing = (_cardValues.GetCardIndexes(highestCard) - _cardValues.GetCardIndexes(lowestCard)) == 4;

                if (isStraightWithAceHighIndexing)
                {
                    return highestCard;
                } else
                {
                    if (highestCard == "A")
                    {
                        var aceLowHand = faces.GetRange(1, 4).ToList();
                        var aceLowHighestCard = aceLowHand[0];
                        var isStraightWithAceLowIndexing = (_cardValues.GetAceLowIndexes(aceLowHighestCard) - _cardValues.GetAceLowIndexes("A")) == 4;

                        if (isStraightWithAceLowIndexing)
                        {
                            return "5";
                        } else
                        {
                            return null;
                        }
                    } else
                    {
                        return null;
                    }
                }
            } else
            {
                return null;
            }
        }

        private int CountMatches(List<(string, int)> list, int quantity)
        {
            return list.Aggregate(0, (total, next) => next.Item2 == quantity ? total + 1 : total); 
        }
    }

    public class HandWithData
    {
        public List<string> Hand;
        public int Value;
        public string Type;
    }
}
