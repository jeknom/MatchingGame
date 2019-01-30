namespace Match
{
    public struct Block
    {
        public enum Color
        {
            None,
            Blue,
            Yellow,
            Green,
            Red
        }

        public enum Bomb
        {
            None,
            Area,
            Horizontal,
            Vertical
        }

        public Color BlockColor;
        public Bomb BombType;
    }
}