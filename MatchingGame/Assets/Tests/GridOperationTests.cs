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
        [Test]
        public void Fill_GridIsNull_ThrowsAnException()
        {
            Assert.Throws<ArgumentNullException>
                (() =>
                {
                    CellGrid cellGrid = null;
                    GridOperation.Fill(cellGrid);
                });
        }

        [Test]
        public void Fill_CellGridQueueIsNotEmpty_ThrowsAnException()
        {
            Assert.Throws<InvalidOperationException>
                (() =>
                {
                    var cellGrid = new CellGrid(2, 2);
                    cellGrid.Events.Enqueue(new AddEvent());
                    GridOperation.Fill(cellGrid);
                });
        }

        [Test]
        public void RemoveCells_GridIsNull_ThrowsAnException()
        {
            Assert.Throws<ArgumentNullException>
                (() =>
                {
                    CellGrid cellGrid = null;
                    List<Point> fakePositions = new List<Point>();
                    GridOperation.RemoveCells(cellGrid, fakePositions);
                });
        }

        [Test]
        public void RemoveCells_CellGridQueueIsNotEmpty_ThrowsAnException()
        {
            Assert.Throws<InvalidOperationException>
                (() =>
                {
                    var cellGrid = new CellGrid(2, 2);
                    cellGrid.Events.Enqueue(new AddEvent());
                    List<Point> fakePositions = new List<Point>();
                    GridOperation.RemoveCells(cellGrid, fakePositions);
                });
        }
    }
}
