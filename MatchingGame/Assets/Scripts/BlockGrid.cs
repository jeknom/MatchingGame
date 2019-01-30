using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Match
{
    public class BlockGrid
    {
        private List<List<Block>> blocks = new List<List<Block>>();
        public readonly int width;
        public readonly int height;

        public List<List<Block>> Blocks { get { return this.blocks; } }
        public Queue<Event> Events { get; }

        public BlockGrid(int width, int height)
        {
            this.width = width;
            this.height = height;

            for (var x = 0; x < this.width; x++)
                this.blocks.Add(new List<Block>());
        }

        public void Fill()
        {
            //Remove undefined
            foreach (var column in this.blocks)
                column.RemoveAll(b => b.Equals(new Block()));

            foreach (var column in this.blocks)
                while (column.Count < this.height)
                    if (Random.Range(0, 100) <= Settings.BombChance)
                        column.Add(new Block
                        {
                            BombType = (Block.Bomb)Random.Range(1, 3)
                        });
                    else
                        column.Add(new Block
                        {
                            BlockColor = (Block.Color)Random.Range(1, 4)
                        });
        }

        public void UndefineBlock(Utils.Point position)
        {
            blocks[position.x][position.y] = new Block
            {
                BlockColor = Block.Color.None,
                BombType = Block.Bomb.None
            };
        }

        public void UndefineBlock(List<Utils.Point> positions)
        {
            foreach (var position in positions)
                blocks[position.x][position.y] = new Block
                {
                    BlockColor = Block.Color.None,
                    BombType = Block.Bomb.None
                };
        }
    }
}