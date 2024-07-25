using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : InteractorBase
{
    //action consumes glumbocoin to either start or continue

    [SerializeField] float GC_costToStart;
    [SerializeField] float USD_costToStart;
    [SerializeField] float GC_costPerSecond;
    [SerializeField] float USD_costPerSecond;
    [SerializeField] AssetInfo[] assetStartCost;
    [SerializeField] AssetInfo[] assetCostPerSecond;
    [SerializeField] Main main;
    [SerializeField] float progressPerSecond;
    [SerializeField] InteractorBase completeInteraction;
    [SerializeField] bool repeat;
    [SerializeField] string text;
    Renderer rend;
    bool running;
    float progress;
    private void Start()
    {
        running = false;
        progress = 0f;
        rend = GetComponent<Renderer>();
        rend.material.SetFloat("progress", progress);
    }

    public override void Trigger()
    {
        StartAction();
    }

    public string GetText()
    {
        return text;
    }
    private void Update()
    {
        if (running)
        {
            AssetInfo[] perSec = assetCostPerSecond;
            if (main.AddRemoveAssets(perSec, true)) {
                AdvanceBar();
                rend.material.SetFloat("progress", progress);
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
        progress = 0f;
        rend.material.SetFloat("progress", progress);
        running = false;
        if (repeat)
        {
            StartAction();
        }
    }

    public void StartAction()
    {
        if (main.AddRemoveAssets(assetStartCost)&&!running)
        {
            running = true;
            progress = 0f;
            Debug.Log("floopy");
        }
        Debug.Log("flart");
    }
}
