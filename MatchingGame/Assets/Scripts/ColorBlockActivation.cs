using System.Collections.Generic;

namespace MatchModel
{
    public class ColorBlockActivation : IBlockActivation
    {
        private readonly List<List<Block>> blocks;
        private Utils.Point position;
        private readonly Block.Color color;

        public ColorBlockActivation(List<List<Block>> blocks, Utils.Point position, Block.Color color)
        {
            this.blocks = blocks;
            this.position = position;
            this.color = color;
        }

        public List<List<Block>> Activate()
        {
            this.blocks[position.x][position.y] = new Block();
            return this.blocks;
        }
    }
}