using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game
{
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
                    Play(level);
                }
            }
        }

        void Play(LevelData data)
        {
            this.StartCoroutine(this.LevelIntroRoutine(data.levelName));

            var gameBoard = new Gameboard(width: 5, height: 5);
        }

        IEnumerator LevelIntroRoutine(string levelName)
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
