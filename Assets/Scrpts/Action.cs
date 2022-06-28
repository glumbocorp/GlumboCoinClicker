using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    //action consumes glumbocoin to either start or continue

    [SerializeField] float costToStart;
    [SerializeField] float costPerSecond;
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
    public string GetText()
    {
        return text;
    }
    private void Update()
    {
        if (running)
        {
            AssetInfo[] perSec = assetCostPerSecond;
            for(int i = 0; i < perSec.Length; i++)
            {
                perSec[i].baseAmt *= Time.deltaTime;
                perSec[i].randomExtra *= Time.deltaTime;
            }
            if (main.AddRemoveMaterialsAndAssets(perSec, costPerSecond * Time.deltaTime)) {
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
        if (main.AddRemoveMaterialsAndAssets(assetStartCost, costToStart)&&!running)
        {
            running = true;
            progress = 0f;
            Debug.Log("floopy");
        }
        Debug.Log("flart");
    }
}
