using UnityEngine;

namespace MatchingGame
{
    public static class GridOperations
    {
        public static void FillGrid()
        {
            foreach (var column in GameGrid.Columns)
                while (column.Count < GameGrid.Height)
                    column.Add(BlockOperations.RandomizeBlock());
        }
    }
}