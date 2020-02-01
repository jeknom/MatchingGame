﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game
{
    public struct Block
    {
        public enum Type
        {
            Red,
            Yellow,
            Blue,
            Green
        }

        public Type type;
    }

    public class Gameboard
    {
        public struct Coord
        {
            public int x;
            public int y;
        }

        List<Block> blocks = new List<Block>();
        int width;
        int height;

        public Gameboard(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        int ToIndex(Coord coord)
        {
            return coord.y * this.width + coord.x;
        }

        Coord ToCoord(int index)
        {
            return new Coord()
            {
                y = index / this.width,
                x = index % this.width
            };
        }
    }

    public class Level : MonoBehaviour
    {
        [SerializeField] List<LevelData> levels = new List<LevelData>();
        [SerializeField] Text levelNameText;
        [SerializeField] Transform levelNameTextStartPosition;
        [SerializeField] Transform levelNameTextEndPosition;

        void Start()
        {
            foreach(var level in this.levels)
            {
                if (!level.hasBeenWon)
                {
                    PlayLevel(level);
                }
            }
        }

        void PlayLevel(LevelData data)
        {
            this.StartCoroutine(this.ShowLevelIntro(data.levelName));
        }

        IEnumerator ShowLevelIntro(string levelName)
        {
            this.levelNameText.text = levelName;

            yield return DOTween
                .Sequence()
                .Append(this.levelNameText.transform.DOMove(
                    endValue: this.levelNameTextEndPosition.position,
                    duration: 1f))
                .WaitForCompletion();

            yield return new WaitForSeconds(2f);

            yield return this.levelNameText.transform.DOScale(
                    endValue: 0f,
                    duration: 1f).WaitForCompletion();

            this.levelNameText.transform.position =
                this.levelNameTextStartPosition.position;
            this.levelNameText.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
