using System.Collections.Generic;

public class Utils
{
    public struct Point
    {
        public int x;
        public int y;
    }

    public static List<Point> GetValidSurrounding(Point target, int width, int height, bool isHexagonal)
    {
        var positions = new List<Point>
        {
            new Point { x = target.x + 1, y = target.y},
            new Point { x = target.x - 1, y = target.y},
            new Point { x = target.x, y = target.y + 1},
            new Point { x = target.x, y = target.y - 1},
        };

        if (isHexagonal)
        {
            positions.AddRange(new List<Point>
            {
                new Point { x = target.x + 1, y = target.y + 1 },
                new Point { x = target.x - 1, y = target.y + 1 },
                new Point { x = target.x + 1, y = target.y - 1 },
                new Point { x = target.x - 1, y = target.y - 1 }
            });
        }

        var returningPositions = new List<Point>();
        foreach (var point in positions)
            if ((point.x >= 0 && point.x < width) && (point.y >= 0 && point.y < height))
                returningPositions.Add(point);

        return returningPositions;
    }
}
