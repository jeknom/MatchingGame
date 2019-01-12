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
        // METHODNAME_CONDITION_EXPECTATION
        [Test]
        public void Fill_CellGridQueueIsNotEmpty_ThrowsAnException()
        {
            var cellGrid = new CellGrid();
            cellGrid.Events.Enqueue(new AddEvent());
            GridOperation.Fill(new CellGrid());
            Assert.Throws<InvalidGridException>
                (delegate { throw new InvalidGridException("Exception"); });
        }
    }
}
