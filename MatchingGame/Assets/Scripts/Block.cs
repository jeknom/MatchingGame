namespace Match
{
    public struct Block
    {
        public enum Color
        {
            None,
            Red,
            Blue,
            Green,
            Yellow,
        }

        public enum BombType
        {
            None,
            Area,
            Horizontal,
            Vertical,
            Xbomb
        }

        public Color colorType;
        public BombType bombType;
        public bool IsUndefined;
    }
}