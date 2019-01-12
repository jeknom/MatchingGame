using NUnit.Framework;
using UnityEngine.TestTools;
using MatchingGame;

namespace Tests
{
    public class CellGridTests
    {
        [Test]
        public void Constructor_InvalidGridSize_ThrowsAnException()
        {
            Assert.Throws<InvalidGridException>
                (() => 
                {
                    var invalidCellGrid = new CellGrid(1, 1);
                    GridOperation.Fill(invalidCellGrid);
                });
        }
    }
}
