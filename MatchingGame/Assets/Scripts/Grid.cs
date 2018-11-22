using System;
using UnityEngine;

public class Grid
{
    private const int _width = 5;
    private const int _height = 6;
    
    private IBlock[,] _blockGrid = new IBlock[_width, _height];
    
    public int Width { get { return _width; } }
    public int Height { get { return _height; } }
    public IBlock[,] Blocks { get { return _blockGrid; } }

    public void Move(IBlock block, int x, int y)
    {
        block.GetObject().transform.position = new Vector3 (x, y, 0);
    }
}