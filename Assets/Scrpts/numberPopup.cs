using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberPopup : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text text;
    [SerializeField] float minInitalYspd;
    [SerializeField] float maxInitalYspd;
    [SerializeField] float minInitalXspd;
    [SerializeField] float maxInitalXspd;
    [SerializeField] float minYspdOverTime;
    [SerializeField] float maxYspdOverTime;
    [SerializeField] float minXspdOverTime;
    [SerializeField] float maxXspdOverTime;
    [SerializeField] float lifetime;
    [SerializeField]AnimationCurve sizeOverTime;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [SerializeField] bool hasColor = true;
    float time = 0f;
    float xSpd; float ySpd;
    float xVel; float yVel;
    Vector3 origin;
    
    void Awake()
    {
        origin = transform.position;
        xSpd = Random.Range(minInitalXspd, maxInitalXspd);
        ySpd = Random.Range(minInitalYspd, maxInitalYspd);
        xVel = Random.Range(minXspdOverTime, maxXspdOverTime);
        yVel = Random.Range(minYspdOverTime, maxYspdOverTime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speed = Vector2.zero;
        time += Time.deltaTime;
        speed.x = xSpd * time + .5f * xVel * Mathf.Pow(time, 2);
        speed.y = ySpd * time + .5f * yVel * Mathf.Pow(time, 2);
        //D = v*t + 1/2 * a * t^2
        if(time > lifetime)
        {
            Destroy(gameObject);
        }
        transform.position = origin + (Vector3)speed;
        text.material.SetColor("Face Color", Color.Lerp(startColor,endColor,time/lifetime));
        float scale = sizeOverTime.Evaluate(time / lifetime);
        text.transform.localScale = new Vector3(scale, scale, scale);
    }
    public void SetNumValue(float number)
    {
        if(number >= 0)
        {
            text.text = "+" + number.ToString();
            if (hasColor)
            {
                text.color = Color.green;
            }
        }
        else
        {
            text.text = number.ToString();
            if (hasColor)
            {
                text.color = Color.red;
            }
        }
        
        text.ForceMeshUpdate();
    }
}
