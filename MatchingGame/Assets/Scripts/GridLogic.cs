using UnityEngine;
using System.Collections.Generic;

namespace Match
{
    public class GridLogic
    {
        private List<List<Block>> blocks = new List<List<Block>>();
        private Queue<Change> changes = new Queue<Change>();

        public Queue<Change> Changes
        {
            get
            {
                var changesCopy = new Queue<Change>(this.changes);
                this.changes.Clear();
                return changesCopy;
            }
        }

        public struct Change
        {
            public enum Type
            {
                Add,
                Remove
            }

            public Type type;
            public Utils.Point position;
            public Block block;
        }

        public GridLogic()
        {
            while (this.blocks.Count < Settings.GridWidth)
                this.blocks.Add(new List<Block>());
        }

        public void Morph()
        {
            // Remove all blocks with default values.
            foreach (var column in this.blocks)
                column.RemoveAll(b => b.Equals(new Block()));

            foreach (var column in this.blocks)
                while (column.Count < Settings.GridHeight)
                {
                    var randomBlock = RandomizedColorBlock();
                    var change = new Change
                    {
                        type = Change.Type.Add,
                        position = new Utils.Point
                        {
                            x = this.blocks.IndexOf(column),
                            y = column.Count
                        },
                        block = randomBlock
                    };

                    // Order matters, change height is picked up from column count.
                    this.changes.Enqueue(change);
                    column.Add(randomBlock);
                }
        }

        private Block RandomizedColorBlock()
        {
            return new Block { blockColor = (Block.Color)Random.Range(1, 5), blockLogic = new ColorActivation() };
        }
    }
}