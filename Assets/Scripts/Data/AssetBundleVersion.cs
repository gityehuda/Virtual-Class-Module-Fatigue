using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/AssetBundleVersion")]
public class AssetBundleVersion : ScriptableObject
{
    public List<AssetBundleNamesAndVersion> assetBundleNamesAndVersions;

    private void OnValidate()
    {
        foreach (var assetBundle in assetBundleNamesAndVersions)
        {
            if (assetBundle != null)
            {
                assetBundle.GenerateAssetBundleUrl();
            }
        }
    }
}
