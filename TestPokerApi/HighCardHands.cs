using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestPokerApi
{
    public class HighCardHands
    {
        // [Theory]
        // [InlineData(new List<List<string>> { new List<string> { "AD", "AH", "AD", "AD", "3D" }, new List<string> { "2C", "3C", "4C", "5C", "7C" } })]
        public void TestHighCardHands(List<List<string>> hands)
        {
            Assert.Equal(4, 2 + 2);
        }
    }
}
