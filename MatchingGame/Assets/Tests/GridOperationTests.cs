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
            Assert.Throws<InvalidGridException>
                (delegate 
                {
                    var cellGrid = new CellGrid();
                    cellGrid = null;
                    GridOperation.Fill(cellGrid);
                });
        }

        [Test]
        public void Fill_CellGridQueueIsNotEmpty_ThrowsAnException()
        {
            Assert.Throws<InvalidGridException>
                (delegate 
                {
                    var cellGrid = new CellGrid();
                    cellGrid.Events.Enqueue(new AddEvent());
                    GridOperation.Fill(cellGrid);
                });
        }
    }
}
