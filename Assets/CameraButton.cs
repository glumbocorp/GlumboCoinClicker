using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButton : MonoBehaviour
{
    [SerializeField] InteractorBase interactor;
    public void Click()
    {
        interactor.Trigger();
    }
}
