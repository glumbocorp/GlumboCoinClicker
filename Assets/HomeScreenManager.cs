using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenManager : MonoBehaviour
{
    [SerializeField] List<GameObject> apps;
    [SerializeField] GameObject homeScreen;
    public void GoHome()
    {
        for(int i = 0; i < apps.Count; i++)
        {
            apps[i].SetActive(false);
            homeScreen.SetActive(true);
        }
    }
    public void OpenApp(GameObject app)
    {
        homeScreen.SetActive(false);
        app.SetActive(true);
    }
}
