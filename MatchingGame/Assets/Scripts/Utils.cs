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

        public static bool IsValidPosition(Utils.Point position, int width, int height)
        {
            if ((position.x >= 0 && position.x < width) && (position.y >= 0 && position.y < height))
                    return true;
            else
                return false;
        }

        public static Utils.Point[] SurroundingPositions(Utils.Point target, bool isHexagonal)
        {
            if (isHexagonal)
            {
                return new Utils.Point[]
                {
                    new Utils.Point { x = target.x++, y = target.y },
                    new Utils.Point { x = target.x--, y = target.y },
                    new Utils.Point { x = target.x, y = target.y++ },
                    new Utils.Point { x = target.x, y = target.y-- },

                    new Utils.Point { x = target.x++, y = target.y++ },
                    new Utils.Point { x = target.x--, y = target.y++ },
                    new Utils.Point { x = target.x++, y = target.y-- },
                    new Utils.Point { x = target.x--, y = target.y-- },
                };
            }
            else
                return new Utils.Point[]
                {
                    new Utils.Point { x = target.x++, y = target.y },
                    new Utils.Point { x = target.x--, y = target.y },
                    new Utils.Point { x = target.x, y = target.y++ },
                    new Utils.Point { x = target.x, y = target.y-- },
                };
        }
    }
}
