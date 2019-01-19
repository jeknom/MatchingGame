namespace MatchingGame
{
    public interface IGridEvent
    {
        void Process(CellGrid cellGrid, VisualGrid visualGrid);
    }   
}