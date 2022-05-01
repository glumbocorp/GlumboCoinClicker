using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI coinAmt;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
