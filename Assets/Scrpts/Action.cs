using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    //action consumes glumbocoin to either start or continue

    [SerializeField] float costToStart;
    [SerializeField] float costPerSecond;
    [SerializeField] Main main;
    [SerializeField] float progressPerSecond;
    [SerializeField] InteractorBase completeInteraction;
    bool running;
    float progress;
    private void Start()
    {
        running = false;
        progress = 0f;
    }
    private void Update()
    {
        if (running)
        {
            if (main.AddRemoveCoins(costPerSecond)) {
                AdvanceBar();
            }
        }
    }

    void AdvanceBar()
    {
        progress += Time.deltaTime * progressPerSecond;
        if (progress > 1f)
        {
            Complete();
        }
    }

    void Complete()
    {
        completeInteraction.Trigger();
    }

    void StartAction()
    {
        if (main.AddRemoveCoins(costToStart))
        {
            running = true;
            progress = 0f;
        }
    }

}
