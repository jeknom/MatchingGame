using System.Collections.Generic;

namespace MatchModel
{
    public interface IBlockActivation
    {
        List<List<Block>> Activate();
    }

    public struct Block
    {
        public enum Color
        {
            Undefined,
            Red,
            Blue,
            Green,
            Yellow
        }

        public enum Bomb
        {
            Undefined,
            Horizontal,
            Vertical,
            Area
        }

        public Color color;
        public Bomb bomb;
    }
}