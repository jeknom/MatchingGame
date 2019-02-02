using MatchModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MatchView
{
    public class View : MonoBehaviour
    {
        public bool IsTweening = false;
        private List<List<GameObject>> assets = new List<List<GameObject>>();
        private GameObject undefinedAsset = null;
        [SerializeField] private RectTransform grid = null;
        [SerializeField] private GameObject redBlock = null;
        [SerializeField] private GameObject blueBlock = null;
        [SerializeField] private GameObject greenBlock = null;
        [SerializeField] private GameObject yellowBlock = null;
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

        public async void Sync(Queue<Model.Event> events, float width, float height)
        {
            var removedAssets = new List<GameObject>();
            while (events.Count > 0)
            {
                var blockEvent = events.Dequeue();

                if (blockEvent.type == Model.Event.Type.Remove)
                {
                    var asset = this.assets[blockEvent.position.x][blockEvent.position.y];
                    Destroy(asset.GetComponent<BoxCollider>());
                    removedAssets.Add(asset);
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

            foreach (var removedAsset in removedAssets)
            {
                if (removedAsset.GetComponent<Animator>())
                    removedAsset.GetComponent<Animator>().SetTrigger("OnCollect");

                Destroy(removedAsset, 0.25f);

                foreach (var column in this.assets)
                    foreach (var asset in column)
                        if (column.Contains(removedAsset))
                            column.Remove(removedAsset);
            }

            var tweeningTasks = new List<Task>();
            foreach (var column in this.assets)
                foreach (var asset in column)
                {
                    var rectTransform = asset.GetComponent<RectTransform>();
                    var x = (this.assets.IndexOf(column) - (width - 1) / 2) * this.assetSpacing;
                    var y = (column.IndexOf(asset) - (height - 1) / 2) * this.assetSpacing;
                    var destination = new Vector3(x, y);

                    if (rectTransform.localPosition != destination)
                        tweeningTasks.Add(CascadeBlock(rectTransform, destination));
                }
            this.IsTweening = true;
            await Task.WhenAll(tweeningTasks);
            this.IsTweening = false;
        }

        private async Task CascadeBlock(RectTransform rectTransform, Vector3 destination)
        {

            await Task.Delay(TimeSpan.FromSeconds(0.25f));
            var startTime = Time.time;
            var startPosition = rectTransform.localPosition;
            var journeyLength = Vector3.Distance(rectTransform.localPosition, destination);

            while (rectTransform.localPosition != destination)
            {
                var distanceCovered = (Time.time - startTime) * this.tweenSpeed * 1000;
                var distanceCompletion = distanceCovered / journeyLength;
                rectTransform.localPosition = Vector3.Lerp(startPosition, destination, distanceCompletion);

                await Task.Delay(1);
            }
        }

        private GameObject ToAsset(Block block)
        {
            GameObject asset;

            if (block.color == Block.Color.Red)
                asset = this.redBlock;
            else if (block.color == Block.Color.Blue)
                asset = this.blueBlock;
            else if (block.color == Block.Color.Green)
                asset = this.greenBlock;
            else if (block.color == Block.Color.Yellow)
                asset = this.yellowBlock;
            else
                asset = this.undefinedAsset;

            return asset;
        }

        public Utils.Point ToPoint(GameObject asset)
        {
            foreach (var column in this.assets)
                if (column.Contains(asset))
                {
                    var assetX = this.assets.IndexOf(column);
                    var assetY = column.IndexOf(asset);

                    return new Utils.Point { x = assetX, y = assetY };
                }

            throw new System.ArgumentException("The view does not contain the given asset.");
        }
    }
}