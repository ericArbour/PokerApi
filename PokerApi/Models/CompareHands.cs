using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class CompareHands
    {
        public List<HandWithData> AssignTypesAndValues(List<List<string>> hands)
        {
            return hands.Select(hand => new HandWithData { Hand = hand, Type = "test", Value = 10000000 }).ToList();
        }
    }

    public class HandWithData
    {
        public List<string> Hand;
        public string Type;
        public int Value;
    }
}
