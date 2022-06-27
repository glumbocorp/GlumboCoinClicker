using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadJRApp : InteractorBase
{
    [SerializeField] Main main;
    public override void Trigger()
    {
        main.SetJRApp(true);
    }
}
