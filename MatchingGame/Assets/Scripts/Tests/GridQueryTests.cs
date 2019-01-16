using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MatchingGame;

namespace Tests
{
    public class GridQueryTests
    {
        [Test]
        public void ToPoint_InvalidCellArgument_ThrowsAnException()
        {
            Assert.Throws<InvalidOperationException>
                (() =>
                {
                    var cellGrid = new CellGrid(2, 2);
                    var fakeCell = new BasicBlock();
                    GridQuery.ToPoint(cellGrid, fakeCell);
                });
        }

        [UnityTest]
        public IEnumerator ToPoint_InvalidBlockArgument_ThrowsAnException()
        {
            Assert.Throws<InvalidOperationException>
                (() =>
                {
                    var visualGrid = GameObject.FindGameObjectWithTag("GameManager").GetComponent<VisualGrid>();
                    var block = new GameObject();
                    GridQuery.ToPoint(visualGrid, block);
                });

            yield return null;
        }

        [Test]
        public void GetSurrounding_ValidCellGrid_ReturnsCorrectPositions()
        {
            var cellGrid = new CellGrid(2, 2);
            GridOperation.Fill(cellGrid);

            var surrounding = GridQuery.GetSurrounding(cellGrid, new Point { x = 0, y = 0 }, false);
            var positionArray = new Point[] {
                new Point { x = 1, y = 0},
                new Point { x = 0, y = 1},
            };

            foreach (var position in positionArray)
                if (!surrounding.Contains(position))
                    throw new MissingMemberException("Surrounding positions did not contain the required members.");
        }
    }
}