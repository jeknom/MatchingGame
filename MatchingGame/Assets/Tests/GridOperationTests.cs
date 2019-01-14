using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MatchingGame;

namespace Tests
{
    // METHODNAME_CONDITION_EXPECTATION
    public class GridOperationTests
    {
        public GridOperationTests()
        {
            UnityEngine.Assertions.Assert.raiseExceptions = true;
        }

        [Test]
        public void Fill_GridIsNull_ThrowsAnException()
        {
            Assert.Throws<AssertionException>
                (() =>
                {
                    CellGrid cellGrid = null;
                    GridOperation.Fill(cellGrid);
                });
        }

        [Test]
        public void Fill_CellGridQueueIsNotEmpty_ThrowsAnException()
        {
            Assert.Throws<AssertionException>
                (() =>
                {
                    var cellGrid = new CellGrid(2, 2);
                    var fakePoint = new Point { x = 0, y = 0 };
                    cellGrid.Events.Enqueue(new AddEvent(fakePoint));
                    GridOperation.Fill(cellGrid);
                });
        }
    }
}
