namespace MatchingGame
{
    public interface ICell
    {
        BlockType Type { get; set; }
        void Activate(CellGrid grid);
    }
}