namespace MatchingGame
{
    using UnityEngine;
    using System.Collections.Generic;

    public static class BlockOperations
    {
        private const int BlackBombChance = 7;

        public static IBlock RandomizeBlock()
        {
            var blocks = new List<IBlock>();
            var randomNumber = Random.Range(0, 100);

            if (randomNumber <= BlackBombChance)
                blocks.Add(new BlackBomb());
            else
                blocks.Add(new Square());

            return blocks[Random.Range(0, blocks.Count)];
        }

        public static void RemoveBlocks(GameGrid grid, List<IBlock> blocks)
        {
            foreach (var block in blocks)
                grid.Columns.ForEach(b => { b.Remove(block); });
        }
    }
}