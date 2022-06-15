using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAppFunctionality : InteractorBase
{
    [SerializeField] GameObject app;
    [SerializeField] HomeScreenManager homeScreen;
    public override void Trigger()
    {
        homeScreen.OpenApp(app);
    }
}
