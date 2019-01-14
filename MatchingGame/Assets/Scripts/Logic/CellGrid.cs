using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace MatchingGame
{
    public class CellGrid
    {
        private int width;
        private int height;
        private List<List<ICell>> columns = new List<List<ICell>>();
        private Queue<IGridEvent> events = new Queue<IGridEvent>();

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public List<List<ICell>> Columns { get { return columns; } }
        public Queue<IGridEvent> Events { get { return events; } }

        public CellGrid(int gridWidth, int gridHeight)
        {
            Assert.AreEqual<int>(2, gridWidth, "The grid has to be at least two Cells wide.");
            Assert.AreEqual<int>(2, gridHeight, "The grid has to be at least two Cells tall.");

            this.width = gridWidth;
            this.height = gridHeight;

            for (var x = 0; x < width; x++)
                Columns.Add(new List<ICell>());
        }
    }
}
