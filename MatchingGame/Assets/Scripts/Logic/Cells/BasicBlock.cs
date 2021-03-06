using UnityEngine;
using System.Collections.Generic;

namespace MatchingGame
{
    public class BasicBlock : ICell
    {
        public BlockType Type { get; set; }

        public BasicBlock()
        {
            Type = (BlockType)Random.Range(0, 3);
        }

        public List<Point> GetPositionsOrDefault(CellGrid grid)
        {
            Debug.Assert(grid != null, "Cannot activate BasicBlock on a null CellGrid.");

            var cellPoint = GridQuery.ToPoint(grid, this);
            var cell = grid.Columns[cellPoint.x][cellPoint.y];
            var queue = new Queue<Point>();
            queue.Enqueue(cellPoint);

            var positions = new List<Point>();
            while (queue.Count > 0)
            {
                var position = queue.Dequeue();
                var currentCell = grid.Columns[position.x][position.y];
                
                if (!positions.Contains(position) && currentCell.Type == cell.Type)
                {
                    positions.Add(position);
                    
                    var surroundingPositions = GridQuery.GetSurrounding(grid, position, false);
                    foreach (var point in surroundingPositions)
                        if (!queue.Contains(point))
                            queue.Enqueue(point);
                }
            }
            if (positions.Count > 1)
            {
                var soundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>();
                Debug.Assert(soundManager != null, "Could not locate SoundManager");
                soundManager.PlaySound(this.Type);

                return positions;
            }

            return null;
        }
    }
}