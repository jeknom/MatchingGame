using UnityEngine;

namespace MatchingGame
{
    public class AddEvent : IGridEvent
    {
        private Point position;
        private BlockType blockType;

        public Point Position { get { return position; } set { position = value; } }
        public BlockType Block { get { return blockType; } set { blockType = value; } }

        public void Unload(CellGrid cellGrid, VisualGrid visualGrid)
        {
            blockType = cellGrid.Columns[position.x][position.y].Type;
            visualGrid.InstantiateCellAt(blockType, position.x);
        }
    }
}