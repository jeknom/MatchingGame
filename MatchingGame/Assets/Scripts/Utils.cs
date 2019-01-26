using UnityEngine;
using System.Collections.Generic;

namespace Match
{
    public struct Vector2Int
    {
        public int x;
        public int y;
    }

    public static class Utils
    {
        public static int ToIndex(int x, int y, int width)
        {
            return y * width + x;
        }

        public static Vector2Int ToVector2Int(int index, int width)
        {
            return new Vector2Int { x = index % width, y = index / width };
        }

        public static void Swap(BlockGrid blockGrid, int index0, int index1)
        {
            blockGrid.Blocks[index0] = blockGrid.Blocks[index1];
            blockGrid.Blocks[index1] = blockGrid.Blocks[index0];
        }

        public static List<Block> DefineBlockGrid(BlockGrid blockGrid)
        {
            List<Block> definedGrid = blockGrid.Blocks;

            for (var i = 0; i < definedGrid.Count; i++)
                if (definedGrid[i].IsUndefined)
                {
                    definedGrid[i] = new Block
                    {
                        colorType = (Block.Color)Random.Range(1, 5),
                        bombType = Block.BombType.None,
                        IsUndefined = false
                    };
                }

            return definedGrid;
        }
    }
}
