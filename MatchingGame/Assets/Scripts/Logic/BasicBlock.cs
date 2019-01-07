using System;
using System.Collections.Generic;

namespace MatchingGame
{
    public class BasicBlock : ICell
    {
        public BlockType Type { get; set; }

        public BasicBlock()
        {
            var random = new Random();
            Type = (BlockType)random.Next(0, 3);
        }

        public void Activate(CellGrid grid)
        {
            var queue = new Queue<Point>();
            var positions = new List<Point>();

            var cellPoint = GridOperations.ToPoint(grid, this);
            var cell = grid.Columns[cellPoint.x][cellPoint.y];
            queue.Enqueue(cellPoint);

            while (queue.Count > 0)
            {
                var position = queue.Dequeue();
                var currentCell = grid.Columns[position.x][position.y];

                if (!positions.Contains(position) && currentCell.Type == cell.Type)
                {
                    positions.Add(position);
                    
                    var surroundingPositions = GridQuery.GetSurrounding(grid, position, false);
                    foreach (var point in surroundingPositions)
                        queue.Enqueue(point);
                }
            }

            GridOperations.RemoveCells(grid, positions);
        }
    }
}