using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace MatchingGame
{
    public class GridOperation
    {
        public static void Fill(CellGrid grid)
        {
            Debug.Assert(grid != null, "Cannot fill a null CellGrid.");
            Debug.Assert(grid.Events.Count == 0, "CellGrid Events queue needs to be cleared before filling it.");

            foreach (var column in grid.Columns)
                while (column.Count < grid.Height)
                {
                    var cell = RandomizeCell();
                    column.Add(cell);

                    var point = new Point();
                    point.x = grid.Columns.IndexOf(column);
                    point.y = column.IndexOf(cell);
                    
                    var gridEvent = new AddEvent(point);
                    grid.Events.Enqueue(gridEvent);
                }
        }

        public static void RemoveCells(CellGrid grid, List<Point> positions)
        {
            Debug.Assert(grid != null && positions != null, "Cannot remove Cells from a null CellGrid.");
            Debug.Assert(grid.Events.Count == 0, "CellGrid Events queue needs to be cleared before removing Cells from it.");

            var cells = new List<ICell>();
            foreach (var point in positions)
            {
                var cell = grid.Columns[point.x][point.y];
                grid.Events.Enqueue(new RemoveEvent(point));
                cells.Add(grid.Columns[point.x][point.y]);
            }

            foreach (var cell in cells)
            {
                var columnQuery =   
                    from column in grid.Columns
                    where column.Contains(cell)
                    select column;

                var containingColumn = columnQuery.SingleOrDefault();
                containingColumn.Remove(cell);
            }
        }

        private static ICell RandomizeCell()
        {
            var value = UnityEngine.Random.Range(1, 100);
            var bombChance = 3;

            if (value > bombChance)
                return new BasicBlock();
            else
                return new BombBlock();
        }
    }
}