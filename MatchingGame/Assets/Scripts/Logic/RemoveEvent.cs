using System.Linq;
using UnityEngine;

namespace MatchingGame
{
    public class RemoveEvent : IGridEvent
    {
        private Point position;
        private BlockType blockType;

        public Point Position { get { return this.position; } set { this.position = value; } }
        public BlockType Block { get { return this.blockType; } set { this.blockType = value; } }

        public RemoveEvent(Point position)
        {
            this.position = position;
        }

        public void Process(CellGrid cellGrid, VisualGrid visualGrid)
        {
            var block = visualGrid.Columns[this.position.x][this.position.y];
            visualGrid.RemoveBuffer.Enqueue(block);
        }
    }
}