using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UninstalJRApp : InteractorBase
{
    [SerializeField] Main main;
    public override void Trigger()
    {
        main.SetJRApp(false);
    }
}
