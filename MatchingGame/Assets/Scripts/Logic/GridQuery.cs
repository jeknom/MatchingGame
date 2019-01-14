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
            Debug.Assert(grid != null, "Cannot get surrounding from a null CellGrid.");
            var isWithinGrid = (target.x >= 0 || target.x < grid.Width) || (target.y >= 0 || target.y < grid.Height);
            Debug.Assert(isWithinGrid, "The cell needs to exist within the CellGrid");

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
            Debug.Assert(grid != null, "Cannot convert to a point when the CellGrid is null.");
            Debug.Assert(cell != null, "Cannot convert a null Cell into a point.");

            foreach (var column in grid.Columns)
                if (column.Contains(cell))
                {
                    Point point;
                    point.x = grid.Columns.IndexOf(column);
                    point.y = column.IndexOf(cell);

                    return point;
                }

            throw new InvalidOperationException("The CellGrid does not contain the given cell.");
        }

        public static Point ToPoint(VisualGrid grid, GameObject block)
        {
            Debug.Assert(grid != null, "Cannot convert to a point when the VisualGrid is null.");
            Debug.Assert(block != null, "Cannot convert a null GameObject into a point.");

            foreach (var column in grid.Columns)
                if (column.Contains(block))
                {
                    var pointX = grid.Columns.IndexOf(column);
                    var pointY = column.IndexOf(block);
                    var point = new Point { x = pointX, y = pointY};

                    return point;
                }

            throw new InvalidOperationException("The GameObject needs to exist on the visual matrix.");
        }
    }
}