using System.Linq;
using System.Collections.Generic;

namespace MatchingGame
{
    public class GridOperations
    {
        public static void Fill(CellGrid grid)
        {
            if (grid.Events.Count > 0)
                throw new InvalidGridException("Cannot add cells to the grid when the event queue is not empty.");

            foreach (var column in grid.Columns)
                while (column.Count < grid.Height)
                {
                    var cell = new BasicBlock();
                    column.Add(cell);

                    var point = new Point();
                    point.x = grid.Columns.IndexOf(column);
                    point.y = column.IndexOf(cell);
                    
                    var gridEvent = new AddEvent { Position = point };
                    grid.Events.Enqueue(gridEvent);
                }
        }

        public static void RemoveCells(CellGrid grid, List<Point> positions)
        {
            if (grid.Events.Count > 0)
                throw new InvalidGridException("Cannot remove cells from grid when the event queue is not empty.");

            var cells = new List<ICell>();

            foreach (var point in positions)
            {
                if ((grid.Columns.Count < point.x && point.x < 0) && (grid.Columns[point.x].Count < point.y && point.x < 0))
                        throw new InvalidGridException("The cell is out of range.");
                
                var cell = grid.Columns[point.x][point.y];
                grid.Events.Enqueue(new RemoveEvent { Position = point });
                cells.Add(grid.Columns[point.x][point.y]);
            }

            foreach (var cell in cells)
            {
                var columnQuery =   
                    from column in grid.Columns
                    where column.Contains(cell)
                    select column;

                var containingColumn = columnQuery.SingleOrDefault();

                if (containingColumn == null)
                    throw new InvalidGridException("The cell does not exist within the grid.");
                else
                    containingColumn.Remove(cell);
            }
        }
    }
}