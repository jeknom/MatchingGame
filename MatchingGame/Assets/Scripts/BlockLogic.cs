using System.Collections.Generic;

namespace Match
{
    public interface IBlockLogic
    {
        List<List<Block>> Activate(List<List<Block>> blocks, Utils.Point position);
    }

    public class ColorActivation : IBlockLogic
    {
        public List<List<Block>> Activate(List<List<Block>> blocks, Utils.Point activatedPosition)
        {
            var queue = new Queue<Utils.Point>();
            var matchingBlocks = new List<Utils.Point>();
            queue.Enqueue(activatedPosition);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if(!matchingBlocks.Contains(current))
                {
                    matchingBlocks.Add(current);

                    foreach (var position in Utils.SurroundingPositions(current, false))
                        if (Utils.IsValidPosition(position, blocks.Count, blocks[0].Count)
                            && blocks[activatedPosition.x][activatedPosition.y].blockColor == blocks[current.x][current.y].blockColor)
                                queue.Enqueue(position);
                }
            }

            foreach (var point in matchingBlocks)
                blocks[point.x][point.y] = new Block();

            return blocks;
        }
    }
}
