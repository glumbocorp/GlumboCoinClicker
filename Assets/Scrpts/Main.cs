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
    float currentUpdateTime = 0f;
    float currentJRusersPercent = 0f;
    float TotalJRusers = 500;
    float activeJRusers = 1;
    float glumbocoins = 0;
    public float Coins => glumbocoins;
    private void Start()
    {
        UpdateAmt();
    }
    public bool AddRemoveCoins(float amount)
    {
        if(glumbocoins + amount >= 0)
        {
            glumbocoins += amount;
            UpdateAmt();
            return true;
        }
        return false;
    }
    public void changeTotalUsers(float relativeUsers)
    {
        TotalJRusers += relativeUsers;
        if (TotalJRusers < 0f)
        {
            TotalJRusers = 0f;
        }
        if (activeJRusers < TotalJRusers)
        {
            activeJRusers = TotalJRusers;
        }
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



            /*if (Random.Range(0, 1f) > 0.6)
            {
                TotalJRusers = Mathf.Max(TotalJRusers + Mathf.Round(Random.Range( - 1, 5)), 1);
                if(TotalJRusers > 100)
                {
                    TotalJRusers -= Mathf.Round(Random.Range(-0.2f, 0.1f) * TotalJRusers);
                }
                else if (TotalJRusers > 500)
                {
                    TotalJRusers -= Mathf.Round(Random.Range(-0.1f, 0.2f) * TotalJRusers);
                }
                else if (TotalJRusers > 1000)
                {
                    TotalJRusers -= Mathf.Round(Random.Range(-0.2f, 0.3f) * TotalJRusers);
                }
                else if (TotalJRusers > 2000)
                {
                    TotalJRusers -= Mathf.Round(Random.Range(-0.3f, 0.4f) * TotalJRusers);
                }
            }
            */
            float currentActive = activeJRusers;
            float currentInact = TotalJRusers - activeJRusers;
            currentJRusersPercent = Mathf.Round(activeJRusers/TotalJRusers * 100f) / 100f;

            if (currentJRusersPercent < 0.2)
            {
                activeJRusers += Mathf.Round(Random.Range(-TotalJRusers / 50f, TotalJRusers / 10f));
            }
            else if (currentJRusersPercent > .8)
            {
                activeJRusers += Mathf.Round(Random.Range(-TotalJRusers / 10f, TotalJRusers / 50f));
            }
            else
            {
                activeJRusers += Mathf.Round(Random.Range(-TotalJRusers / 20f, TotalJRusers / 20f));
            }
            /*
            if (currentJRusersPercent <= 0.5f)//not enough, download
            {
                activeJRusers += Mathf.Round(currentInact * increaseAmountA);
                activeJRusers -= Mathf.Round(currentActive * increaseAmountB);
            }
            else if (currentJRusersPercent > 0.5f)//too many, uninstal
            {
                activeJRusers += Mathf.Round(currentInact * increaseAmountB);
                activeJRusers -= Mathf.Round(currentActive * increaseAmountA);
            }*/
            
            activeJRusers = Mathf.Clamp(activeJRusers, 0f, TotalJRusers);
            currentJRusersPercent = Mathf.Round(activeJRusers / TotalJRusers * 100f) / 100f;

            JRinfo.text = "JR instances:"+activeJRusers.ToString()+"/"+TotalJRusers.ToString()+"\n"+(Mathf.Round(currentJRusersPercent*100f)).ToString()+"%";
            graph.AddNew(activeJRusers, TotalJRusers);
        }
        else
        {
            currentUpdateTime -= Time.deltaTime;
        }
    }
}
