namespace MatchingGame
{
    public interface IGridEvent
    {
        BlockType Block { get; set; }
        Point Position { get; set; }
        void Unload(CellGrid cellGrid, VisualGrid visualGrid);
    }   
}