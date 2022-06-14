using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI coinAmt;
    [SerializeField] TMPro.TextMeshProUGUI coinAmtR;
    [SerializeField] TMPro.TextMeshProUGUI coinAmtB;
    [SerializeField] TMPro.TextMeshProUGUI JRinfo;
    [SerializeField] float minUpdateTime;
    [SerializeField] float maxUpdateTime;
    [SerializeField] [Range(0f, 1f)] float minUpdateAmount;
    [SerializeField] [Range(0f, 1f)] float maxUpdateAmount;
    [SerializeField] [Range(0f, 1f)] float minUpdateAmountLesser;
    [SerializeField] [Range(0f, 1f)] float maxUpdateAmountLesser;
    [SerializeField] GraphRender graph;
    [SerializeField] float regularUpdateInterval;
    float regularUpdateTime = 0f;
    float currentUpdateTime = 0f;
    float currentJRusersPercent = 0f;
    float TotalJRusers = 0;
    float activeJRusers = 1;
    double glumbocoins = 0;
    public double Coins => glumbocoins;
    private void Start()
    {
        UpdateAmt();
    }
    public bool CanAfford(int amount)
    {
        return amount > glumbocoins;
    }
    public bool PurchaseAmount(int amount)
    {
        if (CanAfford(amount))
        {
            AddRemoveCoins(amount);
            return true;
        }
        return false;
    }
    public void AddRemoveCoins(int amount)
    {
        glumbocoins += amount;
        UpdateAmt();
    }

    public void UpdateAmt()
    {
        coinAmt.text = "Glumbocoins: " + glumbocoins.ToString();
        coinAmtR.text = "Glumbocoins: " + glumbocoins.ToString();
        coinAmtB.text = "Glumbocoins: " + glumbocoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentUpdateTime <= 0)
        {
            currentUpdateTime = Random.Range(minUpdateTime, maxUpdateTime);
            float increaseAmountA = Random.Range(minUpdateAmount, maxUpdateAmount);//greater
            float increaseAmountB = Random.Range(minUpdateAmountLesser, maxUpdateAmountLesser);//lesser
            if (Random.Range(0, 1f) > 0.6)
            {
                TotalJRusers = Mathf.Max(TotalJRusers + Mathf.Round(Random.Range(-TotalJRusers * .6f - 1, TotalJRusers * 1.01f + 3)), 1);
                if(TotalJRusers > 100000)
                {
                    TotalJRusers -= Mathf.Round(Random.Range(0.1f, 0.2f) * TotalJRusers);
                }
            }
            float currentActive = activeJRusers;
            float currentInact = TotalJRusers - activeJRusers;
            currentJRusersPercent = Mathf.Round(activeJRusers/TotalJRusers * 100f) / 100f;
            if (currentJRusersPercent < 0.5f)//not enough, download
            {
                activeJRusers += Mathf.Round(currentInact * increaseAmountA);
                activeJRusers -= Mathf.Round(currentActive * increaseAmountB);
            }
            else if (currentJRusersPercent > 0.5f)//too many, uninstal
            {
                activeJRusers += Mathf.Round(currentInact * increaseAmountB);
                activeJRusers -= Mathf.Round(currentActive * increaseAmountA);
            }
            activeJRusers = Mathf.Clamp(activeJRusers, 0f, TotalJRusers);
            currentJRusersPercent = Mathf.Round(activeJRusers / TotalJRusers * 100f) / 100f;

            JRinfo.text = "JR instances:"+activeJRusers.ToString()+"/"+TotalJRusers.ToString()+"\n"+(Mathf.Round(currentJRusersPercent*100f)).ToString()+"%";
        }
        else
        {
            currentUpdateTime -= Time.deltaTime;
        }
        
        if(regularUpdateTime < 0f)
        {
            regularUpdateTime = regularUpdateInterval;
            graph.AddNew(activeJRusers, TotalJRusers);
        }
        else
        {
            regularUpdateTime -= Time.deltaTime;
        }
    }
}
