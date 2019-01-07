namespace MatchingGame
{
    public class GridEvent
    {
        public BlockType Block { get; set; }
        public EventType Event { get; set; }
        public Point Position { get; set; }

        public GridEvent(EventType eventType, BlockType blockType, Point position)
        {
            Event = eventType;
            Block = blockType;
            Position = position;
        }
    }
}