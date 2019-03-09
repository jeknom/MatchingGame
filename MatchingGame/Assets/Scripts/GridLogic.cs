using UnityEngine;
using System.Collections.Generic;

namespace Match
{
    public class GridLogic
    {
        private List<List<Block>> blocks = new List<List<Block>>();
        private List<Change> changes = new List<Change>();

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

        public void ActivateBlock(Utils.Point target)
        {
            this.blocks = this.blocks[target.x][target.y].blockLogic.Activate(this.blocks, target);
        }

        public void Morph()
        {
            foreach (var column in this.blocks)
            {
                foreach (var block in column)
                    if (block.Equals(new Block()))
                        this.changes.Add(new Change
                        {
                            type = Change.Type.Remove,
                            position = Utils.ToPoint(block, this.blocks),
                            block = block
                        });

                column.RemoveAll(b => b.Equals(new Block()));
            }

            foreach (var column in this.blocks)
                while (column.Count < Settings.GridHeight)
                {
                    var randomBlock = RandomizedColorBlock();
                    column.Add(randomBlock);

                    this.changes.Add(new Change
                    {
                        type = Change.Type.Add,
                        position = Utils.ToPoint(randomBlock, this.blocks),
                        block = randomBlock
                    });
                }

            //var debuggi = "";
            //for (var y = Settings.GridHeight - 1; y > 0; y--)
            //    for (var w = 0; w < Settings.GridWidth - 1; w++)
            //    {
            //        if (w == Settings.GridWidth - 2)
            //            debuggi = debuggi + this.blocks[w][y].blockColor.ToString() + "\n";
            //        else
            //            debuggi = debuggi + this.blocks[w][y].blockColor.ToString() + ", ";
            //    }

            //Debug.Log(debuggi);
        }

        private Block RandomizedColorBlock()
        {
            return new Block { blockColor = (Block.Color)Random.Range(1, 5), blockLogic = new ColorActivation() };
        }
    }
}