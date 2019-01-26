using UnityEngine;
using UnityEngine.UI;

namespace Match
{
    public class BlockAsset : MonoBehaviour
    {
        private Block block;
        private Vector3 destination = new Vector3(0, 0);
        private RectTransform rectTransform;
        private Image image;
        [SerializeField] private float speed = 10;

        public Vector3 Destination { get { return this.destination; } set { this.destination = value; } }

        private void Start()
        {
            this.rectTransform = this.GetComponent<RectTransform>();
            this.image = this.GetComponent<Image>();
        }

        private void Update()
        {
            if (this.rectTransform.position != this.destination)
                this.rectTransform.position = Vector3.MoveTowards(this.rectTransform.position, this.destination, this.speed * Time.deltaTime);
        }

        private void Refresh()
        {

        }
    }
}