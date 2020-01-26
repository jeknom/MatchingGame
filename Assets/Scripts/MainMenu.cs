using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Game.Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] Text title;
        [SerializeField] Button startButton;
        [SerializeField] string nextSceneName;

        void Start()
        {
            this.startButton.onClick.AddListener(() =>
                SceneManager.LoadScene(this.nextSceneName));

            var titleSequence = DOTween
                .Sequence()
                .Append(this.title.transform.DORotate(
                    endValue: new Vector3(10f, 45f),
                    duration: 1f,
                    RotateMode.Fast))
                .Append(this.title.transform.DORotate(
                    endValue: new Vector3(0f, 0f),
                    duration: 0.5f,
                    RotateMode.Fast))
                .Append(this.title.transform.DORotate(
                    endValue: new Vector3(10f, -45),
                    duration: 1f,
                    RotateMode.Fast))
                .Append(this.title.transform.DORotate(
                    endValue: new Vector3(0f, 0f),
                    duration: 0.5f,
                    RotateMode.Fast))
                .SetLoops(-1, LoopType.Yoyo);

            DOTween.Play(titleSequence);
        }
    }
}