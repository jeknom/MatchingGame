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
        public bool IsSyncing = false;
        private List<List<GameObject>> assets = new List<List<GameObject>>();
        private GameObject undefinedAsset = null;
        [SerializeField] private RectTransform grid = null;
        [SerializeField] private GameObject redBlock = null;
        [SerializeField] private GameObject blueBlock = null;
        [SerializeField] private GameObject greenBlock = null;
        [SerializeField] private GameObject yellowBlock = null;
        [SerializeField] private float tweenSpeed = 300f;
        [SerializeField] private float assetSpacing = 110f;

        public void Sync(Queue<Model.Event> events, int width, int height)
        {
            this.IsSyncing = true;

            if (this.assets.Count < width)
            {
                this.grid.sizeDelta = Vector3.Scale(this.grid.sizeDelta, new Vector3(width + 1.5f, height + 1.75f));
                this.grid.localPosition = this.grid.localPosition;
                this.grid.localPosition = this.grid.localPosition + new Vector3((width - 1) * (this.assetSpacing / 2), (height - 1) * (this.assetSpacing / 2));

                for (var x = 0; x < width; x++)
                    this.assets.Add(new List<GameObject>());
            }

            var removedBlocks = new List<GameObject>();
            while (events.Count > 0)
            {
                var currentEvent = events.Dequeue();

                if (currentEvent.type == Model.Event.Type.Add)
                {
                    var startPosition = new Vector3((currentEvent.position.x - width / 2 + .5f) * this.assetSpacing, 10f * this.assetSpacing);
                    var asset = Instantiate(ToAsset(currentEvent.block), startPosition, Quaternion.identity);

                    asset.GetComponent<RectTransform>().SetParent(this.grid, false);
                    this.assets[currentEvent.position.x].Add(asset);
                }
                else if (currentEvent.type == Model.Event.Type.Remove)
                    removedBlocks.Add(this.assets[currentEvent.position.x][currentEvent.position.y]);
            }

            foreach (var asset in removedBlocks)
                foreach (var column in this.assets)
                    if (column.Contains(asset))
                    {
                        if (asset.GetComponent<Animator>())
                            asset.GetComponent<Animator>().SetTrigger("OnCollect");

                        column.Remove(asset);
                        Destroy(asset.GetComponent<BoxCollider>());
                        Destroy(asset, 0.25f);
                    }

            var tweeningTasks = new List<Task>();
            foreach (var column in this.assets)
                foreach (var asset in column)
                {
                    var x = this.assets.IndexOf(column);
                    var y = column.IndexOf(asset);
                    var destination = new Vector3((x - width / 2 + .5f) * this.assetSpacing, (column.IndexOf(asset) - height / 2 + .5f) * this.assetSpacing);
                    var tweenTask = TweenAsset(this.assets[this.assets.IndexOf(column)][column.IndexOf(asset)].GetComponent<RectTransform>(), destination);
                    tweeningTasks.Add(tweenTask);
                }

            Task.WhenAll(tweeningTasks);
            this.IsSyncing = false;
        }

        private async Task TweenAsset(RectTransform rectTransform, Vector3 destination)
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

            throw new ArgumentException("The view does not contain the given asset.");
        }
    }
}