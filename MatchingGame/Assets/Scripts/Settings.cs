using System;
using UnityEngine;

namespace Match
{
    public static class Settings
    {
        public static int GridGapSize = 140;
        public static Vector3 BlockSize = new Vector3(1.3f, 1.3f);
        public static TimeSpan SpawnDelay = TimeSpan.FromSeconds(1);
    }
}