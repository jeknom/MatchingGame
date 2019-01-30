using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    private List<List<GameObject>> assets = new List<List<GameObject>>();

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