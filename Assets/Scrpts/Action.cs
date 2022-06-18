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
    [SerializeField] bool repeat;
    Renderer rend;
    bool running;
    float progress;
    private void Start()
    {
        running = false;
        progress = 0f;
        rend = GetComponent<Renderer>();
    }
    private void Update()
    {
        if (running)
        {
            if (main.AddRemoveCoins(costPerSecond)) {
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
        if (main.AddRemoveCoins(costToStart)&&!running)
        {
            running = true;
            progress = 0f;
            Debug.Log("floopy");
        }
        Debug.Log("flart");
    }
}
