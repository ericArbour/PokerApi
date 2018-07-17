using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class CardValues
    {
        private readonly Hashtable FiveKVal = new Hashtable() { { "2", 1 }, { "3", 2 }, { "4", 3 }, { "5", 5 }, { "6", 8 }, { "7", 14 }, { "8", 25 }, { "9", 47 }, { "T", 89 }, { "J", 170 }, { "Q", 329 }, { "K", 639 }, { "A", 1242 } };

        private readonly Hashtable TwoKVal = new Hashtable() { { "2", 1 }, { "3", 2 }, { "4", 3 }, { "5", 5 }, { "6", 8 }, { "7", 13 }, { "8", 21 }, { "9", 34 }, { "T", 55 }, { "J", 89 }, { "Q", 144 }, { "K", 233 }, { "A", 377 } };

        private readonly Hashtable OneKVal = new Hashtable() { { "2", 1 }, { "3", 2 }, { "4", 3 }, { "5", 4 }, { "6", 5 }, { "7", 6 }, { "8", 7 }, { "9", 8 }, { "T", 9 }, { "J", 10 }, { "Q", 11 }, { "K", 12 }, { "A", 13 } };

        private readonly Hashtable CardIndexes = new Hashtable() { { "2", 0 }, { "3", 1 }, { "4", 2 }, { "5", 3 }, { "6", 4 }, { "7", 5 }, { "8", 6 }, { "9", 7 }, { "T", 8 }, { "J", 9 }, { "Q", 10 }, { "K", 11 }, { "A", 12 } };

        private readonly Hashtable AceLowIndexes = new Hashtable() { { "A", 0 }, { "2", 1 }, { "3", 2 }, { "4", 3 }, { "5", 4 }, { "6", 5 }, { "7", 6 }, { "8", 7 }, { "9", 8 }, { "T", 9 }, { "J", 10 }, { "Q", 11 }, { "K", 12 } };

        private int FaceLookup(string face, Hashtable hash)
        {
            if (hash.ContainsKey(face))
            {
                return (int)hash[face];
            } else
            {
                return 0;
            }
        }

        public int GetFiveKVal(string face)
        {
            return FaceLookup(face, FiveKVal);
        }

        public int GetTwoKVal(string face)
        {
            return FaceLookup(face, TwoKVal);
        }

        public int GetOneKVal(string face)
        {
            return FaceLookup(face, OneKVal);
        }

        public int GetCardIndexes(string face)
        {
            return FaceLookup(face, CardIndexes);
        }

        public int GetAceLowIndexes(string face)
        {
            return FaceLookup(face, AceLowIndexes);
        }
    }
}
