using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Gameboard
    {
        public struct Coord
        {
            public int x;
            public int y;
        }

        List<Block> blocks = new List<Block>();
        int width;
        int height;

        public Gameboard(int width, int height)
        {
            this.width = width;
            this.height = height;

            var boardSize = ToIndex(new Coord() {
                x = this.width,
                y = this.height });

            for (var i = 0; i < boardSize; i++)
            {
                var randomColorBlock = GenerateRandomColorBlock();

                this.blocks.Add(randomColorBlock);
            }
        }

        Block GenerateRandomColorBlock()
        {
            return new Block() { type = (Block.Type)Random.Range(1, 4) };
        }

        int ToIndex(Coord coord)
        {
            return coord.y * this.width + coord.x;
        }

        Coord ToCoord(int index)
        {
            return new Coord()
            {
                y = index / this.width,
                x = index % this.width
            };
        }
    }
}
