using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MatchingGame;

namespace Tests
{
    public class CellGridTests
    {
        [Test]
        public void Constructor_InvalidSize_ThrowsAnException()
        {
            Assert.Throws<ArgumentOutOfRangeException>
                (() =>
                {
                    var cellGrid = new CellGrid(1, 1);
                });
        }
    }
}