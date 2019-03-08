using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match
{
    public class GridManager : MonoBehaviour
    {
        private GridLogic logic = new GridLogic();
        private List<List<GameObject>> assets = new List<List<GameObject>>();
        private bool IsInitialized = false;
        [SerializeField] private GameObject grid;
        [SerializeField] private GameObject colorBlock;

        private void Start()
        {
            while (this.assets.Count < Settings.GridWidth)
                this.assets.Add(new List<GameObject>());
        }

        private void Update()
        {
            
            if (!IsCascading())
            {
                this.logic.Morph();
                var changeQueue = this.logic.Changes;
                var removedAssets = new List<GameObject>();

                foreach (var change in changeQueue)
                    if (change.type == GridLogic.Change.Type.Remove)
                        removedAssets.Add(this.assets[change.position.x][change.position.y]);

                foreach (var removedAsset in removedAssets)
                    foreach (var column in this.assets)
                        column.Remove(removedAsset);


                while (changeQueue.Count > 0)
                {
                    var change = changeQueue.Dequeue();
                    if (change.type == GridLogic.Change.Type.Add)
                        this.assets[change.position.x].Add(Instantiate(ToGameObject(change.block), this.grid.transform));
                }
            }

            Cascade();
        }

        private void Cascade()
        {
            if (!IsCascading() || !this.IsInitialized)
                foreach (var column in this.assets)
                    foreach (var asset in column)
                    {
                        asset.transform.localPosition = new Vector3(this.assets.IndexOf(column) * .6f, 8f);
                        StartCoroutine(Move(asset, new Vector3(this.assets.IndexOf(column) * .6f, column.IndexOf(asset) * .6f)));
                    }

            this.IsInitialized = true;
        }

        private bool IsCascading()
        {
            foreach (var column in this.assets)
                foreach (var asset in column)
                    if (asset.transform.localPosition != new Vector3(this.assets.IndexOf(column), column.IndexOf(asset)))
                        return true;

            return false;
        }

        private IEnumerator Move(GameObject asset, Vector3 position)
        {
            var startTime = Time.time;
            var startPosition = asset.transform.localPosition;
            var journeyLength = Vector3.Distance(asset.transform.localPosition, position);

            while (asset.transform.localPosition != position)
            {
                var distanceCovered = (Time.time - startTime) * 3f;
                var distanceCompletion = distanceCovered / journeyLength;
                asset.transform.localPosition = Vector3.Lerp(startPosition, position, distanceCompletion);

                yield return new WaitForSeconds(0.01f);
            }
        }

        private GameObject ToGameObject(Block block)
        {
            if (block.blockColor == Block.Color.Blue)
                this.colorBlock.GetComponent<SpriteRenderer>().color = new Color32(66, 134, 244, 255);
            else if (block.blockColor == Block.Color.Green)
                this.colorBlock.GetComponent<SpriteRenderer>().color = new Color32(109, 209, 52, 255);
            else if (block.blockColor == Block.Color.Red)
                this.colorBlock.GetComponent<SpriteRenderer>().color = new Color32(209, 51, 51, 255);
            else if (block.blockColor == Block.Color.Yellow)
                this.colorBlock.GetComponent<SpriteRenderer>().color = new Color32(255, 246, 0, 255);

            return this.colorBlock;
        }
    }
}