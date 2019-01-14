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
            Assert.IsNotNull<CellGrid>(grid, "Cannot fill a null grid object.");
            Assert.AreEqual<int>(0, grid.Events.Count, "The event queue needs to be empty before filling the grid.");

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
            Debug.Assert(grid != null, "Cannot remove cells from a null CellGrid object.");
            Debug.Assert(grid.Events.Count == 0, "The Events queue needs to be empty before removing cells.");

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