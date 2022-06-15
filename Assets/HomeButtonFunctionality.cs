using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButtonFunctionality : InteractorBase
{
    [SerializeField] HomeScreenManager homeScreen;
    public override void Trigger()
    {
        homeScreen.GoHome();
    }
}
