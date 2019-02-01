using MatchModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MatchView
{
    public class View : MonoBehaviour
    {
        private List<List<GameObject>> assets = new List<List<GameObject>>();
        [SerializeField] private RectTransform backdrop;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private GameObject redBlock;
        [SerializeField] private GameObject blueBlock;
        [SerializeField] private GameObject greenBlock;
        [SerializeField] private GameObject yellowBlock;
        [SerializeField] private float tweenSpeed = 300f;
        [SerializeField] private float assetSpacing = 110f;

        public void Sync(Queue<Model.Event> events)
        {
            var removedAssets = new Queue<GameObject>();
            while (events.Count > 0)
            {
                var blockEvent = events.Dequeue();

                if (blockEvent.type == Model.Event.Type.Remove)
                {
                    var asset = this.assets[blockEvent.position.x][blockEvent.position.y];
                    removedAssets.Enqueue(asset);
                    this.assets[blockEvent.position.x].Remove(asset);
                }
                else if (blockEvent.type == Model.Event.Type.Add)
                {
                    var startPosition = new Vector3(blockEvent.position.x * this.assetSpacing, 9f * this.assetSpacing);
                    var asset = Instantiate(ToAsset(blockEvent.block), startPosition, Quaternion.identity);
                    asset.GetComponent<RectTransform>().SetParent(this.canvas.GetComponent<RectTransform>(), false);
                    this.assets[blockEvent.position.x].Add(asset);
                }
                else if (blockEvent.type == Model.Event.Type.Init)
                    this.assets.Add(new List<GameObject>());
            }

            foreach (var asset in removedAssets)
                Destroy(asset);

            foreach (var column in this.assets)
                foreach (var asset in column)
                {
                    var rectTransform = asset.GetComponent<RectTransform>();
                    var destination = new Vector3(this.assets.IndexOf(column) * this.assetSpacing, column.IndexOf(asset) * this.assetSpacing);

                    if (rectTransform.localPosition != destination)
                        StartCoroutine(CascadeBlock(rectTransform, destination));
                }
        }

        private IEnumerator CascadeBlock(RectTransform rectTransform, Vector3 destination)
        {
            var startTime = Time.time;
            var startPosition = rectTransform.localPosition;
            var journeyLength = Vector3.Distance(rectTransform.localPosition, destination);

            while (rectTransform.localPosition != destination)
            {
                var distanceCovered = (Time.time - startTime) * this.tweenSpeed * 1000;
                var distanceCompletion = distanceCovered / journeyLength;
                rectTransform.localPosition = Vector3.Lerp(startPosition, destination, distanceCompletion);

                yield return null;
            }
        }

        private GameObject ToAsset(Block block)
        {
            GameObject asset = this.redBlock;

            if (block.color == Block.Color.Blue)
                asset = this.blueBlock;
            else if (block.color == Block.Color.Green)
                asset = this.greenBlock;
            else if (block.color == Block.Color.Yellow)
                asset = this.greenBlock;

            return asset;
        }

        public Utils.Point ToPoint(GameObject blockAsset)
        {
            foreach (var column in this.assets)
                if (column.Contains(blockAsset))
                {
                    var assetX = this.assets.IndexOf(column);
                    var assetY = column.IndexOf(blockAsset);

                    return new Utils.Point { x = assetX, y = assetY };
                }

            throw new System.ArgumentException("The view does not contain the given asset.");
        }
    }
}