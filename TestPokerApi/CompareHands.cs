using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerApi.Models;
using Xunit;

namespace TestPokerApi
{
    public class CompareHands
    {
        [Theory]
        [ClassData(typeof(HighCardHandsData))]
        [ClassData(typeof(OnePairHandsData))]
        public void CompareHandsTest(List<string> hand1, List<string> hand2)
        {
            var cardValues = new CardValues();
            var handData = new HandData(cardValues, new HandValues(cardValues));
            var hand1WithData = handData.Calculate(hand1);
            var hand2WithData = handData.Calculate(hand2);
            Assert.True(hand1WithData.Value > hand2WithData.Value);
        }
    }


    public class HighCardHandsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<string> { "AS", "2C", "3C", "4C", "6C" }, new List<string> { "KH", "8D", "TD", "JD", "QH" } };
            yield return new object[] { new List<string> { "KS", "2C", "3C", "4C", "5C" }, new List<string> { "QH", "7D", "9D", "TD", "JH" } };
            yield return new object[] { new List<string> { "QS", "2C", "3C", "4C", "5C" }, new List<string> { "JH", "6D", "8D", "9D", "TH" } };
            yield return new object[] { new List<string> { "JS", "2C", "3C", "4C", "5C" }, new List<string> { "TH", "5D", "7D", "8D", "9H" } };
            yield return new object[] { new List<string> { "TS", "2C", "3C", "4C", "5C" }, new List<string> { "9H", "4D", "6D", "7D", "8H" } };
            yield return new object[] { new List<string> { "9S", "2C", "3C", "4C", "5C" }, new List<string> { "8H", "3D", "5D", "6D", "7H" } };
            yield return new object[] { new List<string> { "8S", "2C", "3C", "4C", "5C" }, new List<string> { "7H", "2D", "4D", "5D", "6H" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class OnePairHandsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<string> { "2S", "2C", "3C", "4C", "5C" }, new List<string> { "AH", "KD", "QD", "JD", "9H" } };
            yield return new object[] { new List<string> { "3S", "3C", "2C", "4C", "5C" }, new List<string> { "2H", "2D", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "4S", "4C", "2C", "3C", "5C" }, new List<string> { "3H", "3D", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "5S", "5C", "2C", "3C", "4C" }, new List<string> { "4H", "4D", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "6S", "6C", "2C", "3C", "4C" }, new List<string> { "5H", "5D", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "7S", "7C", "2C", "3C", "4C" }, new List<string> { "6H", "6D", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "8S", "8C", "2C", "3C", "4C" }, new List<string> { "7H", "7D", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "9S", "9C", "2C", "3C", "4C" }, new List<string> { "8H", "8D", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "TS", "TC", "2C", "3C", "4C" }, new List<string> { "9H", "9D", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "JS", "JC", "2C", "3C", "4C" }, new List<string> { "TH", "TD", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "QS", "QC", "2C", "3C", "4C" }, new List<string> { "JH", "JD", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "KS", "KC", "2C", "3C", "4C" }, new List<string> { "QH", "QD", "AD", "KD", "JH" } };
            yield return new object[] { new List<string> { "AS", "AC", "2C", "3C", "4C" }, new List<string> { "KH", "KD", "AD", "QD", "JH" } };
            yield return new object[] { new List<string> { "TS", "TC", "AC", "2C", "3C" }, new List<string> { "TH", "TD", "KD", "QD", "JH" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
