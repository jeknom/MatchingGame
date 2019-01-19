using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
    public class VisualOperation
    {
        [SerializeField] private static Color32 yellow = new Color32(255, 255, 0, 255);
        [SerializeField] private static Color32 red = new Color32(255, 0, 0, 255);
        [SerializeField] private static Color32 blue = new Color32(0, 0, 255, 255);
        [SerializeField] private static Color32 green = new Color32(0, 255, 0, 255);
        [SerializeField] private static Color32 defaultColour = new Color32(255, 255, 255, 255);

        public static Color32 ToColor(BlockType type)
        {
            if (type == BlockType.Yellow)
                return yellow;
            else if (type == BlockType.Red)
                return red;
            else if (type == BlockType.Blue)
                return blue;
            else if (type == BlockType.Green)
                return green;
            else
                return defaultColour;
        }
    }
}