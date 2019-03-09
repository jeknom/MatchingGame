using UnityEngine;
using System.Collections.Generic;

namespace Match
{
    public static class Utils
    {
        public struct Point
        {
            public int x;
            public int y;
        }

        public static bool IsValidPosition(Point position, int width, int height)
        {
            if ((position.x >= 0 && position.x < width) && (position.y >= 0 && position.y < height))
                return true;
            else
                return false;
        }

        public static Point[] SurroundingPositions(Point target, bool isHexagonal)
        {
            if (isHexagonal)
            {
                return new Point[]
                {
                    new Point { x = target.x + 1, y = target.y },
                    new Point { x = target.x - 1, y = target.y },
                    new Point { x = target.x, y = target.y + 1 },
                    new Point { x = target.x, y = target.y - 1 },

                    new Point { x = target.x + 1, y = target.y + 1 },
                    new Point { x = target.x - 1, y = target.y + 1 },
                    new Point { x = target.x + 1, y = target.y - 1 },
                    new Point { x = target.x - 1, y = target.y - 1 },
                };
            }
            else
                return new Point[]
                {
                    new Point { x = target.x + 1, y = target.y },
                    new Point { x = target.x - 1, y = target.y },
                    new Point { x = target.x, y = target.y + 1 },
                    new Point { x = target.x, y = target.y - 1 },
                };
        }

        public static Point ToPoint(Block block, List<List<Block>> blocks)
        {
            foreach (var column in blocks)
                if (column.Contains(block))
                    return new Point
                    {
                        x = blocks.IndexOf(column),
                        y = column.IndexOf(block)
                    };

            throw new System.ArgumentException("The block was not found.");
        }

        public static Point ToPoint(GameObject block, List<List<GameObject>> blocks)
        {
            foreach (var column in blocks)
                if (column.Contains(block))
                    return new Point
                    {
                        x = blocks.IndexOf(column),
                        y = column.IndexOf(block)
                    };

            throw new System.ArgumentException("The asset was not found.");
        }
    }
}
