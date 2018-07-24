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
        [ClassData(typeof(TwoPairHandsData))]
        [ClassData(typeof(SetHandsData))]
        [ClassData(typeof(StraightHandsData))]
        [ClassData(typeof(FlushHandsData))]
        [ClassData(typeof(FullHouseHandsData))]
        [ClassData(typeof(QuadHandsData))]
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

    public class TwoPairHandsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<string> { "3S", "3C", "2C", "2C", "4C" }, new List<string> { "AH", "AD", "KD", "QD", "JH" } };
            yield return new object[] { new List<string> { "4S", "4C", "2C", "2C", "3C" }, new List<string> { "3H", "3D", "2D", "2D", "AH" } };
            yield return new object[] { new List<string> { "5S", "5C", "2C", "2C", "3C" }, new List<string> { "4H", "4D", "3D", "3D", "AH" } };
            yield return new object[] { new List<string> { "6S", "6C", "2C", "2C", "3C" }, new List<string> { "5H", "5D", "4D", "4D", "AH" } };
            yield return new object[] { new List<string> { "7S", "7C", "2C", "2C", "3C" }, new List<string> { "6H", "6D", "5D", "5D", "AH" } };
            yield return new object[] { new List<string> { "8S", "8C", "2C", "2C", "3C" }, new List<string> { "7H", "7D", "6D", "6D", "AH" } };
            yield return new object[] { new List<string> { "9S", "9C", "2C", "2C", "3C" }, new List<string> { "8H", "8D", "7D", "7D", "AH" } };
            yield return new object[] { new List<string> { "TS", "TC", "2C", "2C", "3C" }, new List<string> { "9H", "9D", "8D", "8D", "AH" } };
            yield return new object[] { new List<string> { "JS", "JC", "2C", "2C", "3C" }, new List<string> { "TH", "TD", "9D", "9D", "AH" } };
            yield return new object[] { new List<string> { "QS", "QC", "2C", "2C", "3C" }, new List<string> { "JH", "JD", "TD", "TD", "AH" } };
            yield return new object[] { new List<string> { "KS", "KC", "2C", "2C", "3C" }, new List<string> { "QH", "QD", "JD", "JD", "AH" } };
            yield return new object[] { new List<string> { "AS", "AC", "2C", "2C", "3C" }, new List<string> { "KH", "KD", "QD", "QD", "AH" } };
            yield return new object[] { new List<string> { "KS", "KC", "3C", "3C", "2C" }, new List<string> { "KH", "KD", "2D", "2D", "AH" } };
            yield return new object[] { new List<string> { "4S", "4C", "3C", "3C", "2C" }, new List<string> { "4S", "4C", "2C", "2C", "AC" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class SetHandsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<string> { "2S", "2C", "2C", "3C", "4C" }, new List<string> { "AH", "AD", "KD", "KD", "QH" } };
            yield return new object[] { new List<string> { "3S", "3C", "3C", "2C", "4C" }, new List<string> { "2H", "2D", "2D", "AD", "KH" } };
            yield return new object[] { new List<string> { "4S", "4C", "4C", "2C", "3C" }, new List<string> { "3H", "3D", "3D", "AD", "KH" } };
            yield return new object[] { new List<string> { "5S", "5C", "5C", "2C", "3C" }, new List<string> { "4H", "4D", "4D", "AD", "KH" } };
            yield return new object[] { new List<string> { "6S", "6C", "6C", "2C", "3C" }, new List<string> { "5H", "5D", "5D", "AD", "KH" } };
            yield return new object[] { new List<string> { "7S", "7C", "7C", "2C", "3C" }, new List<string> { "6H", "6D", "6D", "AD", "KH" } };
            yield return new object[] { new List<string> { "8S", "8C", "8C", "2C", "3C" }, new List<string> { "7H", "7D", "7D", "AD", "KH" } };
            yield return new object[] { new List<string> { "9S", "9C", "9C", "2C", "3C" }, new List<string> { "8H", "8D", "8D", "AD", "KH" } };
            yield return new object[] { new List<string> { "TS", "TC", "TC", "2C", "3C" }, new List<string> { "9H", "9D", "9D", "AD", "KH" } };
            yield return new object[] { new List<string> { "JS", "JC", "JC", "2C", "3C" }, new List<string> { "TH", "TD", "TD", "AD", "KH" } };
            yield return new object[] { new List<string> { "QS", "QC", "QC", "2C", "3C" }, new List<string> { "JH", "JD", "JD", "AD", "KH" } };
            yield return new object[] { new List<string> { "KS", "KC", "KC", "2C", "3C" }, new List<string> { "QH", "QD", "QD", "AD", "KH" } };
            yield return new object[] { new List<string> { "AS", "AC", "AC", "2C", "3C" }, new List<string> { "KH", "KD", "KD", "AD", "QH" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class StraightHandsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<string> { "AS", "2C", "3C", "4C", "5C" }, new List<string> { "AH", "AD", "AD", "KD", "QH" } };
            yield return new object[] { new List<string> { "2S", "3C", "4C", "5C", "6C" }, new List<string> { "AH", "2D", "3D", "4D", "5H" } };
            yield return new object[] { new List<string> { "3S", "4C", "5C", "6C", "7C" }, new List<string> { "2H", "3D", "4D", "5D", "6H" } };
            yield return new object[] { new List<string> { "4S", "5C", "6C", "7C", "8C" }, new List<string> { "3H", "4D", "5D", "6D", "7H" } };
            yield return new object[] { new List<string> { "5S", "6C", "7C", "8C", "9C" }, new List<string> { "4H", "5D", "6D", "7D", "8H" } };
            yield return new object[] { new List<string> { "6S", "7C", "8C", "9C", "TC" }, new List<string> { "5H", "6D", "7D", "8D", "9H" } };
            yield return new object[] { new List<string> { "7S", "8C", "9C", "TC", "JC" }, new List<string> { "6H", "7D", "8D", "9D", "TH" } };
            yield return new object[] { new List<string> { "8S", "9C", "TC", "JC", "QC" }, new List<string> { "7H", "8D", "9D", "TD", "JH" } };
            yield return new object[] { new List<string> { "9S", "TC", "JC", "QC", "KC" }, new List<string> { "8H", "9D", "TD", "JD", "QH" } };
            yield return new object[] { new List<string> { "TS", "JC", "QC", "KC", "AC" }, new List<string> { "9H", "TD", "JD", "QD", "KH" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class FlushHandsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<string> { "2S", "3S", "4S", "5S", "7S" }, new List<string> { "AH", "KD", "QD", "JD", "TH" } };
            yield return new object[] { new List<string> { "2S", "3S", "4S", "6S", "7S" }, new List<string> { "2S", "3S", "4S", "5S", "7S" } };
            yield return new object[] { new List<string> { "2S", "3S", "4S", "5S", "8S" }, new List<string> { "2S", "4S", "5S", "6S", "7S" } };
            yield return new object[] { new List<string> { "2S", "3S", "4S", "5S", "9S" }, new List<string> { "3S", "5S", "6S", "7S", "8S" } };
            yield return new object[] { new List<string> { "2S", "3S", "4S", "5S", "TS" }, new List<string> { "4S", "6S", "7S", "8S", "9S" } };
            yield return new object[] { new List<string> { "2S", "3S", "4S", "5S", "JS" }, new List<string> { "5S", "7S", "8S", "9S", "TS" } };
            yield return new object[] { new List<string> { "2S", "3S", "4S", "5S", "QS" }, new List<string> { "6S", "8S", "9S", "TS", "JS" } };
            yield return new object[] { new List<string> { "2S", "3S", "4S", "5S", "KS" }, new List<string> { "7S", "9S", "TS", "JS", "QS" } };
            yield return new object[] { new List<string> { "2S", "3S", "4S", "6S", "AS" }, new List<string> { "8S", "TS", "JS", "QS", "KS" } };
            yield return new object[] { new List<string> { "9S", "JS", "QS", "KS", "AS" }, new List<string> { "2S", "3S", "4S", "6S", "AS" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class FullHouseHandsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<string> { "2S", "2C", "2C", "3C", "3C" }, new List<string> { "AS", "KS", "QS", "JS", "9S" } };
            yield return new object[] { new List<string> { "3S", "3C", "3C", "4C", "4C" }, new List<string> { "2H", "2D", "2D", "AD", "AH" } };
            yield return new object[] { new List<string> { "4S", "4C", "4C", "2C", "2C" }, new List<string> { "3H", "3D", "3D", "AD", "AH" } };
            yield return new object[] { new List<string> { "5S", "5C", "5C", "2C", "2C" }, new List<string> { "4H", "4D", "4D", "AD", "AH" } };
            yield return new object[] { new List<string> { "6S", "6C", "6C", "2C", "2C" }, new List<string> { "5H", "5D", "5D", "AD", "AH" } };
            yield return new object[] { new List<string> { "7S", "7C", "7C", "2C", "2C" }, new List<string> { "6H", "6D", "6D", "AD", "AH" } };
            yield return new object[] { new List<string> { "8S", "8C", "8C", "2C", "2C" }, new List<string> { "7H", "7D", "7D", "AD", "AH" } };
            yield return new object[] { new List<string> { "9S", "9C", "9C", "2C", "2C" }, new List<string> { "8H", "8D", "8D", "AD", "AH" } };
            yield return new object[] { new List<string> { "TS", "TC", "TC", "2C", "2C" }, new List<string> { "9H", "9D", "9D", "AD", "AH" } };
            yield return new object[] { new List<string> { "JS", "JC", "JC", "2C", "2C" }, new List<string> { "TH", "TD", "TD", "AD", "AH" } };
            yield return new object[] { new List<string> { "QS", "QC", "QC", "2C", "2C" }, new List<string> { "JH", "JD", "JD", "AD", "AH" } };
            yield return new object[] { new List<string> { "KS", "KC", "KC", "2C", "2C" }, new List<string> { "QH", "QD", "QD", "AD", "AH" } };
            yield return new object[] { new List<string> { "AS", "AC", "AC", "2C", "2C" }, new List<string> { "KH", "KD", "KD", "QD", "QH" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class QuadHandsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<string> { "2S", "2C", "2C", "2C", "3C" }, new List<string> { "AH", "AD", "AD", "KD", "KH" } };
            yield return new object[] { new List<string> { "3S", "3C", "3C", "3C", "4C" }, new List<string> { "2H", "2D", "2D", "2D", "AH" } };
            yield return new object[] { new List<string> { "4S", "4C", "4C", "4C", "2C" }, new List<string> { "3H", "3D", "3D", "3D", "AH" } };
            yield return new object[] { new List<string> { "5S", "5C", "5C", "5C", "2C" }, new List<string> { "4H", "4D", "4D", "4D", "AH" } };
            yield return new object[] { new List<string> { "6S", "6C", "6C", "6C", "2C" }, new List<string> { "5H", "5D", "5D", "5D", "AH" } };
            yield return new object[] { new List<string> { "7S", "7C", "7C", "7C", "2C" }, new List<string> { "6H", "6D", "6D", "6D", "AH" } };
            yield return new object[] { new List<string> { "8S", "8C", "8C", "8C", "2C" }, new List<string> { "7H", "7D", "7D", "7D", "AH" } };
            yield return new object[] { new List<string> { "9S", "9C", "9C", "9C", "2C" }, new List<string> { "8H", "8D", "8D", "8D", "AH" } };
            yield return new object[] { new List<string> { "TS", "TC", "TC", "TC", "2C" }, new List<string> { "9H", "9D", "9D", "9D", "AH" } };
            yield return new object[] { new List<string> { "JS", "JC", "JC", "JC", "2C" }, new List<string> { "TH", "TD", "TD", "TD", "AH" } };
            yield return new object[] { new List<string> { "QS", "QC", "QC", "QC", "2C" }, new List<string> { "JH", "JD", "JD", "JD", "AH" } };
            yield return new object[] { new List<string> { "KS", "KC", "KC", "KC", "2C" }, new List<string> { "QH", "QD", "QD", "QD", "AH" } };
            yield return new object[] { new List<string> { "AS", "AC", "AC", "AC", "2C" }, new List<string> { "KH", "KD", "KD", "KD", "QH" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
