namespace MatchingGame
{
    public static class GridOperations
    {
        public static void FillGrid(GameGrid grid)
        {
            foreach (var column in grid.Columns)
                while (column.Count < grid.Height)
                    column.Add(BlockOperations.RandomizeBlock());
        }
    }
}