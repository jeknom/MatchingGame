using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MatchingGame;

namespace Tests
{
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
                    var fakePoint = new Point { x = 0, y = 0 };
                    cellGrid.Events.Enqueue(new AddEvent(fakePoint));
                    GridOperation.Fill(cellGrid);
                });
        }

        [Test]
        public void RemoveCells_ArgumentIsNull_ThrowsAnException()
        {
            Assert.Throws<ArgumentNullException>
                (() =>
                {
                    CellGrid cellGrid = null;
                    var fakeList = new List<Point>();
                    GridOperation.RemoveCells(cellGrid, fakeList);
                });

            Assert.Throws<ArgumentNullException>
                (() =>
                {
                    CellGrid cellGrid = new CellGrid(2, 2);
                    List<Point> fakePositions = null;
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
                    var positions = new List<Point>();
                    var addEvent = new AddEvent(new Point { x = 0, y = 0});
                    cellGrid.Events.Enqueue(addEvent);
                    GridOperation.RemoveCells(cellGrid, positions);
                });
        }
    }
}
