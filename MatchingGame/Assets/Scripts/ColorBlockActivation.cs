using System.Collections.Generic;

namespace MatchModel
{
    public class ColorBlockActivation : IBlockActivation
    {
        private readonly List<List<Block>> blocks;
        private Utils.Point position;
        private readonly Block.Color color;

        public ColorBlockActivation(List<List<Block>> blocks, Utils.Point position)
        {
            this.blocks = blocks;
            this.position = position;
        }

        public List<List<Block>> Activate()
        {
            var queue = new Queue<Utils.Point>();
            var nullingList = new List<Utils.Point>();
            queue.Enqueue(this.position);

            while (queue.Count > 0)
            {
                var target = queue.Dequeue();

                if (this.blocks[target.x][target.y].color == this.blocks[position.x][position.y].color && !nullingList.Contains(target))
                {
                    var validSurroundingPositions = Utils.GetValidSurrounding(target, this.blocks.Count, this.blocks[0].Count, false);

                    foreach (var point in validSurroundingPositions)
                        if (!queue.Contains(point))
                            queue.Enqueue(point);
                            
                    nullingList.Add(target);
                }
            }

            if (nullingList.Count >= 2)
                foreach (var point in nullingList)
                    this.blocks[point.x][point.y] = new Block();

            return this.blocks;
        }
    }
}