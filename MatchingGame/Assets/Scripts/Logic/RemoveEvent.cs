using System;
using System.Linq;
using UnityEngine;

namespace MatchingGame
{
    public class RemoveEvent : IGridEvent
    {
        private Point position;

        public RemoveEvent(Point position)
        {
            Debug.Assert(position.x >= 0 && position.y >= 0, "The position values need to be 0 or higher.");
            this.position = position;
        }

        public void Process(CellGrid cellGrid, VisualGrid visualGrid)
        {
            Debug.Assert(cellGrid != null && visualGrid != null, "Cannot process an AddEvent with a null argument(s).");
            var block = visualGrid.Columns[this.position.x][this.position.y];
            visualGrid.RemoveBuffer.Enqueue(block);
        }
    }
}