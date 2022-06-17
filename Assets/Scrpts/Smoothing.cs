using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Smoothing 
{
    float _currentTime;
    float _endTime;
    smoothingTypes smoothType;
    public enum smoothingTypes
    {
        InFastOutSlow
    }
    public Smoothing(float startTime, float endTime, smoothingTypes smoothType)
    {
        _currentTime = startTime;
        _endTime = endTime;
    }
    public float TickVal(float deltaTime) { //returns 0 to 1 based on smoothing
        Tick(deltaTime);
        return Val();
    }
    public float Val()
    {
        float toReturn = 0f;
        switch (smoothType)
        {
            case smoothingTypes.InFastOutSlow:
                toReturn = Mathf.Sin(Mathf.PI * _currentTime / 2f  /_endTime);
                break;
            default: return 0f;
        }
        if (_currentTime >= _endTime)
        {
            return 1f;
        }
        else
        {
            return toReturn;
        }
    }
    public void Tick(float deltaTime)
    {
        _currentTime += deltaTime;
    }

}
