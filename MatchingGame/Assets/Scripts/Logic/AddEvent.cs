using UnityEngine;

namespace MatchingGame
{
    public class AddEvent : IGridEvent
    {
        private Point position;
        private BlockType blockType;

        public Point Position { get { return this.position; } set { this.position = value; } }
        public BlockType Block { get { return this.blockType; } set { this.blockType = value; } }

        public AddEvent(Point position)
        {
            this.position = position;
        }

        public void Process(CellGrid cellGrid, VisualGrid visualGrid)
        {
            this.blockType = cellGrid.Columns[this.position.x][this.position.y].Type;
            visualGrid.InstantiateCellAt(cellGrid, this.blockType, this.position.x);
        }
    }
}