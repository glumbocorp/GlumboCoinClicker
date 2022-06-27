using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshPro tmp;
    public void SetText(string text)
    {
        tmp.text = text;
    }
    public void Active(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
