using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Match
{
    public class GridLogic : MonoBehaviour
    {
        private BlockGrid blockGrid = new BlockGrid(6, 8);
        private List<GameObject> blockAssets = new List<GameObject>();
        [SerializeField] private GameObject redBlock;
        [SerializeField] private GameObject blueBlock;
        [SerializeField] private GameObject greenBlock;
        [SerializeField] private GameObject yellowBlock;

        private void Start()
        {
            blockGrid.Blocks = Utils.DefineBlockGrid(blockGrid);

            foreach (var block in this.blockGrid.Blocks)
            {
                var blockAsset = Instantiate(ToAsset(block));
                var blockAssetTransform = blockAsset.GetComponent<RectTransform>();
                blockAssetTransform.SetParent(this.GetComponent<RectTransform>(), false);
                blockAssetTransform.localScale = Settings.BlockSize;
                this.blockAssets.Add(blockAsset);
            }
        }

        private void Update()
        {
            InstantCascade();
        }

        public void InstantCascade()
        {
            for (var x = 0; x < this.blockGrid.Width; x++)
                for (var y = 0; y < this.blockGrid.Height; y++)
                {
                    var index = Utils.ToIndex(x, y, this.blockGrid.Width);
                    var destination = new Vector3(x * Settings.GridGapSize, y * Settings.GridGapSize);
                    this.blockAssets[index].GetComponent<RectTransform>().localPosition = destination;
                }
        }

        public GameObject ToAsset(Block block)
        {
            GameObject asset;

            if (block.colorType == Block.Color.Red)
                asset = redBlock;
            else if (block.colorType == Block.Color.Blue)
                asset = blueBlock;
            else if (block.colorType == Block.Color.Green)
                asset = greenBlock;
            else if (block.colorType == Block.Color.Yellow)
                asset = yellowBlock;
            else
                asset = redBlock;

            return asset;
        }
    }
}