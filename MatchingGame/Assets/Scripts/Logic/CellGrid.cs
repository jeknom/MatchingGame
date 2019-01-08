using System.Collections.Generic;

namespace MatchingGame
{
    public class CellGrid
    {
        private int width = 6;
        private int height = 10;
        private List<List<ICell>> columns = new List<List<ICell>>();
        private Queue<IGridEvent> events = new Queue<IGridEvent>();

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public List<List<ICell>> Columns { get { return columns; } set { columns = value; } }
        public Queue<IGridEvent> Events { get { return events; } set { events = value; } }

        public CellGrid()
        {
            for (var x = 0; x < Width; x++)
                Columns.Add(new List<ICell>());
        }
    }
}
