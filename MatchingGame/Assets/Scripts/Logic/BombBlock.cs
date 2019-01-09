using System.Collections.Generic;

namespace MatchingGame
{
    public class BombBlock : ICell
    {
        public BlockType Type { get; set; }

        public BombBlock()
        {
            Type = BlockType.Bomb;
        }

        public void Activate(CellGrid grid)
        {
            var cellPoint = GridQuery.ToPoint(grid, this);
            var cell = grid.Columns[cellPoint.x][cellPoint.y];
            var queue = new Queue<Point>();
            queue.Enqueue(cellPoint);

            var positions = new List<Point>();
            while (queue.Count > 0)
            {
                var position = queue.Dequeue();
                var currentCell = grid.Columns[position.x][position.y];
                
                if (!positions.Contains(position))
                {
                    positions.Add(position);
                    
                    var surroundingPositions = GridQuery.GetSurrounding(grid, position, true);
                    foreach (var point in surroundingPositions)
                    {
                        positions.Add(point);
                    }
                }
            }

            GridOperations.RemoveCells(grid, positions);
        }
    }
}