namespace Match
{
    public struct Block
    {
        public enum Color
        {
            None,
            Blue,
            Green,
            Yellow,
            Red
        }

        public enum BombType
        {
            None,
            Area
        }

        public Color blockColor;
        public BombType bombType;
    }
}
