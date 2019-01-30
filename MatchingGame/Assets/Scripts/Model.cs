using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    public readonly int width;
    public readonly int height;
    private List<List<Block>> blocks = new List<List<Block>>();

    public Model(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void SetInput(Utils.Point point)
    {

    }

    public struct Block
    {

    }
}
