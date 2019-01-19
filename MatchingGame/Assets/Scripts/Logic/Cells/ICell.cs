using System.Collections.Generic;

namespace MatchingGame
{
    public interface ICell
    {
        BlockType Type { get; set; }
        List<Point> GetPositionsOrDefault(CellGrid grid);
    }
}