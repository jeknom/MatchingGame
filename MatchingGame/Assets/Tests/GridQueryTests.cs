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
        public void GetSurrounding_GridIsNull_ThrowsAnException()
        {
            Assert.Throws<ArgumentNullException>
                (()=>
                {
                    CellGrid grid = null;
                    var fakePoint = new Point();
                    GridQuery.GetSurrounding(grid, fakePoint, true);
                });
        }

        [Test]
        public void GetSurrounding_PointIsOutOfRange_ThrowsAnException()
        {
            var cellGrid = new CellGrid(2, 2);
            var invalidPoint1 = new Point {x = -1, y = -1};
            var invalidPoint2 = new Point {x = 3, y = 3};

            Assert.Throws<ArgumentOutOfRangeException>
                (()=>
                {
                    GridQuery.GetSurrounding(cellGrid, invalidPoint1, true);
                });

            Assert.Throws<ArgumentOutOfRangeException>
                (()=>
                {
                    GridQuery.GetSurrounding(cellGrid, invalidPoint2, true);
                });
        }

        [Test]
        public void ToPoint_InvalidArgument_ThrowsAnException()
        {
            Assert.Throws<ArgumentNullException>
                (()=>
                {
                    CellGrid grid = null;
                    GridQuery.ToPoint(grid, new BasicBlock());
                });
            
            Assert.Throws<ArgumentNullException>
                (()=>
                {
                    CellGrid grid = new CellGrid(2, 2);
                    BasicBlock cell = null;
                    GridQuery.ToPoint(grid, cell);
                });
        }
    }
}
