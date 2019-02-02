using System.Collections.Generic;
using UnityEngine;

namespace MatchModel
{
    public class Model
    {
        public readonly int width;
        public readonly int height;
        private List<List<Block>> blocks = new List<List<Block>>();
        private Queue<Event> events = new Queue<Event>();

        public Queue<Event> Events { get { return this.events; } }

        public Model(int width, int height)
        {
            this.width = width;
            this.height = height;

            for (var i = 0; i < this.width; i++)
            {
                blocks.Add(new List<Block>());
                events.Enqueue(new Event { type = Event.Type.Init });
            }

            Fill();
        }

        public void SetInput(Utils.Point point)
        {
            Debug.Assert(this.events.Count == 0, "Events need to be cleared before setting input.");

            var block = this.blocks[point.x][point.y];

            if (block.color != Block.Color.Undefined)
                this.blocks = new ColorBlockActivation(this.blocks, point).Activate();

            RemoveUndefined();
            Fill();
        }

        public void Fill()
        {
            var random = new System.Random();

            foreach (var column in this.blocks)
                while (column.Count < this.height)
                {
                    var randomizedBlock = new Block
                    {
                        color = (Block.Color)random.Next(1, 5),
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
                    this.events.Enqueue(addEvent);
                }
        }

        private void RemoveUndefined()
        {
            foreach (var column in this.blocks)
            {
                foreach (var block in column)
                    if (block.Equals(new Block()))
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
                        this.events.Enqueue(removeEvent);
                    }
                column.RemoveAll(b => b.Equals(new Block()));
            }
                
        }

        public struct Event
        {
            public enum Type
            {
                Init,
                Add,
                Remove
            }
            public Type type;
            public Block block;
            public Utils.Point position;
        }
    }
}