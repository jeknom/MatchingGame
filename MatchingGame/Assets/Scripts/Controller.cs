using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Match
{
    public class Controller : MonoBehaviour
    {
        private BlockGrid blockGrid = new BlockGrid(6, 8);
        private List<List<GameObject>> assets = new List<List<GameObject>>();
        [SerializeField] private GameObject redBlock;
        [SerializeField] private GameObject greenBlock;
        [SerializeField] private GameObject blueBlock;
        [SerializeField] private GameObject yellowBlock;

        private void Start()
        {
            for (var x = 0; x < blockGrid.width; x++)
                this.assets.Add(new List<GameObject>());
        }

        private async void Update()
        {
            GameObject targetBlock = null;

            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    var interaction = hit.collider.gameObject;
                    targetBlock = interaction;
                }
            }

            if (targetBlock != null)
            {
                var targetPoint = ToPoint(targetBlock);
                this.blockGrid.UndefineBlock(targetPoint);
            }
        }

        public async Task InstantiateMissing()
        {
            
        }

        public async Task DestroyUndefined()
        {
            var undefinedAssets = new List<GameObject>();

            foreach (var column in this.blockGrid.Blocks)
                foreach (var block in column)
                {
                    var x = this.blockGrid.Blocks.IndexOf(column);
                    var y = column.IndexOf(block);

                    if (block.Equals(new Block()))
                    {
                        undefinedAssets.Add(this.assets[x][y]);
                        this.assets[x].RemoveAt(y);
                    }
                }

            foreach (var asset in undefinedAssets)
            {
                Destroy(asset);
                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
        }

        public async Task TweenAssetAsync(RectTransform rectTransform, Vector3 destination, float speed, float startTime, float journeyLength)
        {
            var distanceCovered = (Time.time - startTime) * speed;
            var fracJourney = distanceCovered / journeyLength;
            var startPosition = rectTransform.localPosition;

            while (rectTransform.position != destination)
            {
                rectTransform.localPosition = Vector3.Lerp(startPosition, destination, fracJourney);
                await Task.Delay(TimeSpan.FromMilliseconds(1));
            }
        }

        private Utils.Point ToPoint(GameObject asset)
        {
            var column = this.assets.Where(b => b.Contains(asset)).Single();
            var assetX = this.assets.IndexOf(column);
            var assetY = column.IndexOf(asset);

            return new Utils.Point { x = assetX, y = assetY };
        }
    }
}