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
                var input = GetInput();
                
                if (input)
                {
                    var target = Utils.ToPoint(input, this.assets);
                    this.logic.ActivateBlock(target);
                }
            }

            this.logic.Morph();
            var changeQueue = this.logic.Changes;
            foreach (var change in changeQueue)
                Debug.Log(change.type);

            if (changeQueue.Count > 0)
            {
                var destroyed = new List<GameObject>();
                foreach (var change in changeQueue)
                    if (change.type == GridLogic.Change.Type.Remove)
                    {
                        var current = this.assets[change.position.x][change.position.y];
                        destroyed.Add(current);
                        this.assets[change.position.x].Remove(current);
                    }

                foreach (var asset in destroyed)
                    Destroy(asset);

                while (changeQueue.Count > 0)
                {
                    var change = changeQueue.Dequeue();
                    if (change.type == GridLogic.Change.Type.Add)
                    {
                        var newAsset = Instantiate(ToGameObject(change.block), this.grid.transform);
                        newAsset.transform.localPosition = new Vector3(change.position.x * .6f, 6f);
                        this.assets[change.position.x].Add(newAsset);
                    }
                }

                foreach (var column in this.assets)
                    foreach (var asset in column)
                    {
                        var point = Utils.ToPoint(asset, this.assets);
                        StartCoroutine(Move(asset, new Vector3(point.x * .6f, point.y * .6f)));
                    }
            }
        }

        private GameObject GetInput()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out RaycastHit hit))
                return hit.collider.gameObject;
            else
                return null;
        }

        private bool IsCascading()
        {
            foreach (var column in this.assets)
                foreach (var asset in column)
                    if (asset.transform.localPosition != new Vector3(this.assets.IndexOf(column) * .6f, column.IndexOf(asset) * .6f))
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
                var distanceCovered = (Time.time - startTime) * 5f;
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