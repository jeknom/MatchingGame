using System;
using UnityEngine;

namespace Match
{
    public static class Settings
    {
        public static float CascadeSpeed = 500f;
        public static int GridGapSize = 125;
        public static Vector3 BlockSize = new Vector3(1.2f, 1.2f);
        public static TimeSpan SpawnDelay = TimeSpan.FromSeconds(1);
        public static TimeSpan MoveDelay = TimeSpan.FromMilliseconds(1);
    }
}