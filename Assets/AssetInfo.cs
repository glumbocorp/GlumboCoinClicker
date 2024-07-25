using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asset", menuName = "ScriptableObjects/AddScriptableAsset", order = 1)]
public class AssetInfo : ScriptableObject
{
    [SerializeField] public float baseAmt = 1f;
    [SerializeField] public float randomExtra = 0f;
    [SerializeField] public Main.Assets asset;

    public AssetInfo(float _baseAmt, float _randomExtra, Main.Assets _asset)
    {
        baseAmt = _baseAmt;
        randomExtra = _randomExtra;
        asset = _asset;
    }
}
