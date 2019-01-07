using System.Collections.Generic;

namespace MatchingGame
{
    public static class GridQuery
    {
        public static List<Point> GetSurrounding(CellGrid grid, Point target, bool IsHexagonal)
        {
            var positions = new List<Point>();

            positions.Add(new Point{ x = target.x + 1, y = target.y });
            positions.Add(new Point{ x = target.x - 1, y = target.y });
            positions.Add(new Point{ x = target.x, y = target.y + 1 });
            positions.Add(new Point{ x = target.x, y = target.y - 1 });
            
            if (IsHexagonal)
            {
                positions.Add(new Point{ x = target.x + 1, y = target.y + 1 });
                positions.Add(new Point{ x = target.x - 1, y = target.y + 1 });
                positions.Add(new Point{ x = target.x + 1, y = target.y - 1 });
                positions.Add(new Point{ x = target.x - 1, y = target.y - 1 });
            }

            var surrounding = new List<Point>();
            
            foreach (var position in positions)
                if ((position.x >= 0 && position.y < grid.Width) && (position.y >= 0 && position.y < grid.Height))
                    surrounding.Add(position);

            return surrounding;
        }
    }
}