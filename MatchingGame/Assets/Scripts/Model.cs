using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchModel
{
    public class Model
    {
        public readonly int width;
        public readonly int height;
        private List<List<Block>> blocks = new List<List<Block>>();
        private List<Event> events = new List<Event>();

        public Model(int width, int height)
        {
            this.width = width;
            this.height = height;

            for (var i = 0; i < this.width; i++)
                blocks.Add(new List<Block>());

            Fill();
        }

        public void SetInput(Utils.Point point)
        {
            Debug.Assert(events.Count == 0, "Output needs to be taken before setting input.");

            var block = this.blocks[point.x][point.y];

            if (block.color != Block.Color.Undefined)
                this.blocks = new ColorBlockActivation(this.blocks, point, block.color).Activate();

            RemoveUndefined();
            Fill();
        }

        public List<Event> GetOutput()
        {
            var changes = this.events;
            this.events.Clear();

            return changes;
        }

        private void Fill()
        {
            foreach (var column in this.blocks)
                while (column.Count < this.height)
                {
                    var randomizedBlock = new Block
                    {
                        color = (Block.Color)Random.Range(1, 4),
                        bomb = Block.Bomb.Undefined
                    };
                    column.Add(randomizedBlock);

                    var addEvent = new Event
                    {
                        type = Event.Type.Add,
                        block = randomizedBlock,
                        position = new Utils.Point
                        {
                            x = this.blocks.IndexOf(column),
                            y = column.Count - 1
                        }
                    };
                    events.Add(addEvent);
                }
        }

        private void RemoveUndefined()
        {
            foreach (var column in this.blocks)
                foreach (var block in column)
                {
                    var removeEvent = new Event
                    {
                        type = Event.Type.Remove,
                        position = new Utils.Point
                        {
                            x = this.blocks.IndexOf(column),
                            y = column.IndexOf(block)
                        }
                    };
                    events.Add(removeEvent);

                    if (block.Equals(new Block()))
                        column.Remove(block);
                }
        }

        public struct Event
        {
            public enum Type
            {
                Add,
                Remove
            }
            public Type type;
            public Block block;
            public Utils.Point position;
        }
    }
}