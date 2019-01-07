using System.Collections.Generic;

namespace MatchingGame
{
    public class GridOperations
    {
        public static Point ToPoint (CellGrid grid, ICell cell)
        {
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
                    
                    var gridEvent = new GridEvent(EventType.Add, cell.Type, point);
                    grid.Events.Enqueue(gridEvent);
                }
        }

        public static void RemoveCells(CellGrid grid, List<Point> positions)
        {
            if (grid.Events.Count > 0)
                throw new InvalidGridException("Cannot remove cells from grid when the event queue is not empty.");

            foreach (var point in positions)
            {
                if ((grid.Columns.Count < point.x && point.x < 0) && (grid.Columns[point.x].Count < point.y && point.x < 0))
                        throw new InvalidGridException("The cell is out of range.");
                
                var cell = grid.Columns[point.x][point.y];
                grid.Events.Enqueue(new GridEvent(EventType.Remove, cell.Type, point));
                grid.Columns[point.x].RemoveAt(point.y);
            }
        }
    }
}