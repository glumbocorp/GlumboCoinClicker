using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparePhone 
{
    //each phone adds to potential users, collects glumbo individually, can be automated 
    //todo: add market thresholds to enable/disable
    //todo: add ability to sell computing power for money, at the cost of glumbo production, speed at identifying the market's tendency, and speed of instal/uninstal
    float storedGlumbocoin = 0f;
    bool enabled;
    public enum mode
    {
        MANUAL,
        NEGATIVE_ENABLED,//only installed when market is negative 
        POSITIVE_ENABLED,//only installed when market is positive 
    }

    public void Tick(float delta)
    {
        if (enabled) { storedGlumbocoin += delta; };
    }

    public SparePhone()
    {
        storedGlumbocoin = 0f;
        enabled = false;
    }

    public void Toggle()
    {
        SetEnabled(!enabled);
    }

    public void SetEnabled(bool isEnabled)
    {
        enabled = isEnabled;
    }

    public void Purge()
    {
        storedGlumbocoin = 0f;
    }
}
