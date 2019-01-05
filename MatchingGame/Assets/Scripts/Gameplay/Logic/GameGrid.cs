using System.Collections.Generic;

namespace MatchingGame.Logic
{
    public class GameGrid
    {
        private const int width = 6;
        private const int height = 10;
        private List<List<IBlock>> columns = new List<List<IBlock>>();

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public List<List<IBlock>> Columns { get { return columns; } set { columns = value; } }

        public GameGrid()
        {
            for (var x = 0; x < width; x++)
                columns.Add(new List<IBlock>());
        }
    }
}