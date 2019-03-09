using System.Collections.Generic;
using UnityEngine;

namespace Match
{
    public interface IBlockLogic
    {
        List<List<Block>> Activate(List<List<Block>> blocks, Utils.Point position);
    }

    public class ColorActivation : IBlockLogic
    {
        public List<List<Block>> Activate(List<List<Block>> blocks, Utils.Point target)
        {
            var blocksCopy = new List<List<Block>>(blocks);
            var queue = new Queue<Utils.Point>();
            var correspondingPoints = new List<Utils.Point>();
            var targetColor = blocksCopy[target.x][target.y].blockColor;
            queue.Enqueue(target);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var currentColor = blocksCopy[current.x][current.y].blockColor;

                if (!correspondingPoints.Contains(current) && currentColor == targetColor)
                {
                    correspondingPoints.Add(current);
                    var surroundingPoints = Utils.SurroundingPositions(current, false);
                    foreach (var point in surroundingPoints)
                        if (Utils.IsValidPosition(point, Settings.GridWidth, Settings.GridHeight))
                            queue.Enqueue(point);
                }
            }

            foreach (var point in correspondingPoints)
                blocksCopy[point.x][point.y] = new Block();

            return blocksCopy;
        }
    }
}
