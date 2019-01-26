using System.Collections.Generic;
using UnityEngine;

namespace Match
{
    public class BlockGrid
    {
        private List<Block> blocks = new List<Block>();
        private int width;
        private int height;

        public List<Block> Blocks { get { return this.blocks; } set { this.blocks = value; } }
        public int Width { get { return this.width; } }
        public int Height { get { return this.height; } }

        public BlockGrid(int width, int height)
        {
            this.width = width;
            this.height = height;

            for (var i = 0; i < this.width * this.height; i++)
            {
                this.Blocks.Add(new Block { IsUndefined = true });
            }
        }
    }
}