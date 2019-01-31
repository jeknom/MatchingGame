using MatchModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace MatchView
{
    public class View : MonoBehaviour
    {
        private List<List<GameObject>> assets = new List<List<GameObject>>();
        [SerializeField] GameObject redBlock;
        [SerializeField] GameObject blueBlock;
        [SerializeField] GameObject greenBlock;
        [SerializeField] GameObject yellowBlock;

        public async Task Sync(List<Model.Event> events)
        {
            var removedAssets = new List<GameObject>();

            foreach (var blockEvent in events)
            {
                if (blockEvent.type == Model.Event.Type.Remove)
                {
                    var asset = this.assets[blockEvent.position.x][blockEvent.position.y];
                    removedAssets.Add(asset);
                    this.assets[blockEvent.position.x].Remove(asset);
                }
                else if (blockEvent.type == Model.Event.Type.Add)
                {
                    var asset = Instantiate(ToAsset(blockEvent.block));
                    this.assets[blockEvent.position.x].Add(asset);
                }
            }

            foreach (var asset in removedAssets)
                foreach (var column in this.assets)
                    if (column.Contains(asset))
                        column.Remove(asset);

            var moveTasks = new List<Task>();
            foreach (var column in this.assets)
                foreach (var asset in column)
                {
                    var rectTransform = asset.GetComponent<RectTransform>();
                    var destination = new Vector3(this.assets.IndexOf(column), column.IndexOf(asset));

                    if (rectTransform.localPosition != destination)
                        moveTasks.Add(MoveBlock(rectTransform, destination));
                }

            await Task.WhenAll(moveTasks);
        }

        private async Task MoveBlock(RectTransform rectTransform, Vector3 destination)
        {
            var startTime = Time.time;
            var startPosition = rectTransform.localPosition;
            var journeyLength = Vector3.Distance(startPosition, destination);
            var speed = 10f;

            while (rectTransform.localPosition != destination)
            {
                var distanceCovered = (Time.time - startTime) * speed;
                var distanceCompletion = distanceCovered / journeyLength;
                rectTransform.localPosition = Vector3.Lerp(startPosition, destination, distanceCompletion);

                await Task.Delay(1);
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

            throw new System.ArgumentException("The view does not contain the given blockAsset.");
        }
    }
}