using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace MatchingGame
{
    public static class GridQuery
    {
        public static List<Point> GetSurrounding(CellGrid grid, Point target, bool IsHexagonal)
        {
            if (grid == null)
                throw new ArgumentNullException("Cannot get surrounding points form a null grid.");

            if ((target.x < 0 || target.x > grid.Width) || (target.y < 0 || target.y > grid.Height))
                throw new ArgumentOutOfRangeException("The point is out of the range of this grid.");

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
                if ((position.x >= 0 && position.x < grid.Width) && (position.y >= 0 && position.y < grid.Height))
                    surrounding.Add(position);

            return surrounding;
        }

        public static Point ToPoint (CellGrid grid, ICell cell)
        {
            if (grid == null)
                throw new ArgumentNullException("Cannot convert to a point on a cellGrid which is null.");

            if (cell == null)
                throw new ArgumentNullException("Cannot convert a null cell into a point.");

            foreach (var column in grid.Columns)
                if (column.Contains(cell))
                {
                    Point point;
                    point.x = grid.Columns.IndexOf(column);
                    point.y = column.IndexOf(cell);

                    return point;
                }
            
            throw new InvalidGridException("The cell grid does not contain the given cell.");
        }

        public static Point ToPoint(VisualGrid grid, GameObject block)
        {
            if (grid == null)
                throw new ArgumentNullException("Cannot convert to a point on a VisualGrid which is null.");

            if (block == null)
                throw new ArgumentNullException("Cannot convert a null block into a point.");

            foreach (var column in grid.Columns)
                if (column.Contains(block))
                {
                    var pointX = grid.Columns.IndexOf(column);
                    var pointY = column.IndexOf(block);
                    var point = new Point { x = pointX, y = pointY};

                    return point;
                }

            throw new InvalidVisualException("The GameObject needs to exist on the visual matrix.");
        }
    }
}