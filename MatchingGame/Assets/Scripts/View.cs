using MatchModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchView
{
    public class View : MonoBehaviour
    {
        private List<List<GameObject>> assets = new List<List<GameObject>>();
        [SerializeField] private RectTransform grid;
        [SerializeField] private GameObject redBlock;
        [SerializeField] private GameObject blueBlock;
        [SerializeField] private GameObject greenBlock;
        [SerializeField] private GameObject yellowBlock;
        [SerializeField] private float tweenSpeed = 300f;
        [SerializeField] private float assetSpacing = 110f;

        public void SyncGridScale(float width, float height)
        {
            this.grid.sizeDelta = Vector3.Scale(this.grid.sizeDelta, new Vector3(width + 1.5f, height + 1.75f));

            width = width - 1;
            height = height - 1;
            this.grid.localPosition = this.grid.localPosition;
            this.grid.localPosition = this.grid.localPosition + new Vector3(width * (this.assetSpacing / 2), height * (this.assetSpacing / 2));
        }

        public bool IsCascading(float width, float height)
        {
            foreach (var column in this.assets)
                foreach (var asset in column)
                {
                    var x = (this.assets.IndexOf(column) - (width - 1) / 2) * this.assetSpacing;
                    var y = (column.IndexOf(asset) - (height - 1) / 2) * this.assetSpacing;
                    var destination = new Vector3(x, y);

                    if (asset.GetComponent<RectTransform>().localPosition != destination)
                        return true;
                }

            return false;
        }

        public void Sync(Queue<Model.Event> events, float width, float height)
        {
            var removedAssets = new Queue<GameObject>();
            while (events.Count > 0)
            {
                var blockEvent = events.Dequeue();

                if (blockEvent.type == Model.Event.Type.Remove)
                {
                    var asset = this.assets[blockEvent.position.x][blockEvent.position.y];
                    Destroy(asset.GetComponent<BoxCollider>());
                    removedAssets.Enqueue(asset);
                    this.assets[blockEvent.position.x].Remove(asset);
                }
                else if (blockEvent.type == Model.Event.Type.Add)
                {
                    var startPosition = new Vector3((blockEvent.position.x - (width - 1) / 2) * this.assetSpacing, 8f * this.assetSpacing);
                    var asset = Instantiate(ToAsset(blockEvent.block), startPosition, Quaternion.identity);
                    asset.GetComponent<RectTransform>().SetParent(this.grid.GetComponent<RectTransform>(), false);
                    this.assets[blockEvent.position.x].Add(asset);
                }
                else if (blockEvent.type == Model.Event.Type.Init)
                    this.assets.Add(new List<GameObject>());
            }

            while (removedAssets.Count > 0)
            {
                var removedAsset = removedAssets.Dequeue();
                if (removedAsset.GetComponent<Animator>())
                    removedAsset.GetComponent<Animator>().SetTrigger("OnCollect");

                Destroy(removedAsset, 0.25f);
            }

            foreach (var column in this.assets)
                foreach (var asset in column)
                {
                    var rectTransform = asset.GetComponent<RectTransform>();
                    var x = (this.assets.IndexOf(column) - (width - 1) / 2) * this.assetSpacing;
                    var y = (column.IndexOf(asset) - (height - 1) / 2) * this.assetSpacing;
                    var destination = new Vector3(x, y);

                    if (rectTransform.localPosition != destination)
                        StartCoroutine(CascadeBlock(rectTransform, destination));
                }
        }

        private IEnumerator CascadeBlock(RectTransform rectTransform, Vector3 destination)
        {
            yield return new WaitForSeconds(0.25f);
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
                asset = this.yellowBlock;

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