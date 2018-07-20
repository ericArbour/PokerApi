using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class CompareHands
    {
        private CardValues CardValues { get; set; }

        public CompareHands(CardValues cardValues)
        {
            CardValues = cardValues;
        }

        public List<HandWithData> AssignValuesAndTypes(List<List<string>> hands)
        {
            var handValuesAndTypes = hands.Select(hand => GetValuesAndTypes(hand)).ToList();
            return hands.Select((hand, i) => new HandWithData { Hand = hand, Value = handValuesAndTypes[i].Item1, Type = handValuesAndTypes[i].Item2 }).ToList();
        }

        private (int, string) GetValuesAndTypes(List<string> hand)
        {
            var orderedFaces = hand.Select(card => card.Substring(0, 1)).OrderByDescending(face => CardValues.GetCardIndexes(face)).ToList();
            var uniqueFaces = orderedFaces.Distinct().ToList();

            (string, int) uniqueFaceToCountTuple(string uniqueFace) =>
                (uniqueFace, orderedFaces.Where(orderedFace => uniqueFace == orderedFace).ToList().Count());

            var faceCounts = uniqueFaces.Select(uniqueFaceToCountTuple).ToList();

            var pairCount = CountMatches(faceCounts, 2);
            var setCount = CountMatches(faceCounts, 3);
            var quadCount = CountMatches(faceCounts, 4);

            var isStraight = CheckStraight(uniqueFaces);

            return (1, "One Pair");
        }

        private string CheckStraight(List<string> faces)
        {
            if (faces.Count() == 5)
            {

                var highestCard = faces[0];
                var lowestCard = faces[4];
                var isStraightWithAceHighIndexing = (CardValues.GetCardIndexes(highestCard) - CardValues.GetCardIndexes(lowestCard)) == 4;

                if (isStraightWithAceHighIndexing)
                {
                    return highestCard;
                } else
                {
                    if (highestCard == "A")
                    {
                        var aceLowHand = faces.GetRange(1, 4).ToList();
                        var aceLowHighestCard = aceLowHand[0];
                        var isStraightWithAceLowIndexing = (CardValues.GetAceLowIndexes(aceLowHighestCard) - CardValues.GetAceLowIndexes("A")) == 4;

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
