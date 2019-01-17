using System;
using UnityEngine;

namespace MatchingGame
{
    public class AddEvent : IGridEvent
    {
        private Point position;
        private BlockType blockType;

        public AddEvent(Point position)
        {
            Debug.Assert(position.x >= 0 && position.y >= 0, "The position values need to be 0 or higher.");
            this.position = position;
        }

        public void Process(CellGrid cellGrid, VisualGrid visualGrid)
        {
            Debug.Assert(cellGrid != null && visualGrid != null, "Cannot process an AddEvent with a null argument(s).");
            this.blockType = cellGrid.Columns[this.position.x][this.position.y].Type;
            visualGrid.InstantiateCellAt(cellGrid, this.blockType, this.position.x);
        }
    }
}