using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match
{
    public class GridLogic
    {
        const int GridWidth = 8;
        const int GridHeight = 10;
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

        public struct Point
        {
            public int x;
            public int y;
        }

        public struct Change
        {
            public enum Type
            {
                Add,
                Remove
            }

            public Type changeType;
            public Point position;
            public Block block;
        }

        public GridLogic()
        {
            while (this.blocks.Count < GridWidth)
                this.blocks.Add(new List<Block>());
        }

        private void Fill()
        {
            foreach (var column in this.blocks)
                while (column.Count < GridHeight)
                {
                    var randomBlock = RandomizedColorBlock();
                    var change = new Change
                    {
                        changeType = Change.Type.Add,
                        position = new Point
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

        private void Remove(List<Point> positions)
        {
            // Order matters, block is based on data prior the change.
            foreach (var position in positions)
            {
                this.changes.Enqueue(new Change
                {
                    changeType = Change.Type.Remove,
                    position = position,
                    block = this.blocks[position.x][position.y]
                });
                this.blocks[position.x][position.y] = new Block();
            }

            foreach (var column in this.blocks)
                column.RemoveAll(b => b.Equals(new Block()));
        }

        private Block RandomizedColorBlock()
        {
            var random = new System.Random();
            return new Block { blockColor = (Block.Color)random.Next(1, 4) };
        }
    }
}