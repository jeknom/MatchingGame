namespace MatchingGame
{
    public interface IGridEvent
    {
        BlockType Block { get; set; }
        Point Position { get; set; }
        void Process(CellGrid cellGrid, VisualGrid visualGrid);
    }   
}