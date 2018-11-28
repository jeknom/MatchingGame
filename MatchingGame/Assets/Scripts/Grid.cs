using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private const int _width = 5;
    private const int _height = 6;
    private List<List<IBlock>> _blockGrid = new List<List<IBlock>>();
    
    public int Width { get { return _width; } }
    public int Height { get { return _height; } }
    public List<List<IBlock>> Blocks { get { return _blockGrid; } }

    public Grid()
    {
        for (var x = 0; x < _width; x++)
            _blockGrid.Add(new List<IBlock>());
    }

    public void Move(IBlock block, int x, int y)
    {
        block.GetObject().transform.position = new Vector3 (x, y, 0);
    }

    public void RemoveBlock(IBlock block)
    {
        foreach (var row in _blockGrid)
        {
            var target = row.SingleOrDefault(b => b == block);
            
            if (target != null)
            {
                target.Destroy();
                row.Remove(target);
            }
        }
    }

    public void Cascade()
    {
        foreach (var row in _blockGrid)
            row.ForEach(b => b.GetObject().GetComponent<Transform>().position = new Vector3(_blockGrid.IndexOf(row), row.IndexOf(b)));
    }
}