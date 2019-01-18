using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Assert(grid != null, "Cannot activate BombBlock on a null CellGrid.");

            var activatedPoint = GridQuery.ToPoint(grid, this);
            var activationQueue = new Queue<Point>();
            var choppingBlock = new List<Point>();
            activationQueue.Enqueue(activatedPoint);

            while (activationQueue.Count > 0)
            {
                var processing = activationQueue.Dequeue();

                if (!choppingBlock.Contains(processing))
                {
                    var surrounding = GridQuery.GetSurrounding(grid, processing, true);

                    var surroundingBombs = 
                        from point in surrounding
                        where grid.Columns[point.x][point.y].Type == BlockType.Bomb
                        select point;

                    var surroundingOthers =
                        from point in surrounding
                        where grid.Columns[point.x][point.y].Type != BlockType.Bomb
                        select point;

                    foreach (var point in surroundingBombs)
                        activationQueue.Enqueue(point);

                    foreach (var point in surroundingOthers)
                        if (!choppingBlock.Contains(point))
                            choppingBlock.Add(point);

                    choppingBlock.Add(processing);
                }
            }

            GridOperation.RemoveCells(grid, choppingBlock);
            
            var soundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>();
            soundManager.PlaySound(this.Type);
        }
    }
}