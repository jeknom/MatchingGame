using System.Linq;
using UnityEngine;

namespace MatchingGame
{
    public class RemoveEvent : IGridEvent
    {
        private Point position;
        private BlockType blockType;

        public Point Position { get { return position; } set { position = value; } }
        public BlockType Block { get { return blockType; } set { blockType = value; } }

        public void Unload(CellGrid cellGrid, VisualGrid visualGrid)
        {
            visualGrid.RemoveBuffer.Add(position);
        }
    }
}