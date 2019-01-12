using System.Collections.Generic;

namespace MatchingGame
{
    public class CellGrid
    {
        private int width;
        private int height;
        private List<List<ICell>> columns = new List<List<ICell>>();
        private Queue<IGridEvent> events = new Queue<IGridEvent>();

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public List<List<ICell>> Columns { get { return columns; } set { columns = value; } }
        public Queue<IGridEvent> Events { get { return events; } set { events = value; } }

        public CellGrid(int gridWidth, int gridHeight)
        {
            width = gridWidth;
            height = gridHeight;

            if (width < 2 || height < 2)
                throw new InvalidGridException("The grid has to be at least two cell wide and tall.");

            for (var x = 0; x < width; x++)
                Columns.Add(new List<ICell>());
        }
    }
}
