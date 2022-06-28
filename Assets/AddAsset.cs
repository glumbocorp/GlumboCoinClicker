using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAsset : InteractorBase
{

    [SerializeField] AssetInfo[] toAddRemove;
    public override void Trigger()
    {
        Main.staticMain.AddRemoveMaterials(toAddRemove);
    }
}
