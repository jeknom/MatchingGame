using System.Collections.Generic;

namespace MatchingGame
{
    public static class GameGrid
    {
        private const int width = 6;
        private const int height = 10;
        private static List<List<IBlock>> columns = new List<List<IBlock>>();

        public static int Width { get { return width; } }
        public static int Height { get { return height; } }
        public static List<List<IBlock>> Columns { get { return columns; } set { columns = value; } }

        static GameGrid()
        {
            for (var x = 0; x < width; x++)
                columns.Add(new List<IBlock>());
        }
    }
}