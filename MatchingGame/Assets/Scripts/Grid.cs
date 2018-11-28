using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private const int _width = 5;
    private const int _height = 6;
    private const float CascadeSpeed = 0.01f;
    private bool IsCascading;
    private List<List<IBlock>> _blockGrid = new List<List<IBlock>>();
    
    public int Width { get { return _width; } }
    public int Height { get { return _height; } }
    public List<List<IBlock>> Blocks { get { return _blockGrid; } }

    public Grid()
    {
        for (var x = 0; x < _width; x++)
            _blockGrid.Add(new List<IBlock>());
    }

    public IEnumerator MoveBlock(IBlock block, Vector3 destination)
    {
        IsCascading = true;

        while (block.GetObject().transform.position != destination)
        {
            block.GetObject().transform.position = Vector3.MoveTowards(block.GetObject().transform.position, destination, CascadeSpeed);
            yield return null;
        }

        IsCascading = false;
    }

    public void RemoveBlock(IBlock block)
    {
        foreach (var row in _blockGrid)
        {
            var target = row.SingleOrDefault(b => b == block);
            
            if (target != null && !IsCascading)
            {
                target.Destroy();
                row.Remove(target);
            }
        }
    }
}