using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphRender : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    List<float> mins;
    List<float> maxes;
    [SerializeField] int numberOfPoints;

    private void Awake()
    {
        mins = new List<float>();
        maxes = new List<float>();
    }
    public void AddNew(float min, float max)//current and total users
    {
        mins.Add(min);
        maxes.Add(max);
        if (mins.Count> numberOfPoints)
        {
            mins.RemoveAt(0);
            maxes.RemoveAt(0);
        }
        float xstart = -.5f;
        float xend = .5f;
        float ystart = -.5f;
        float yend = .5f;
        Vector3[] points;
        points = new Vector3[mins.Count];
        for(int i = 0; i < mins.Count;i++)
        {
            float x = Mathf.Lerp(xstart, xend, (float)i / (float)numberOfPoints);
            float y;
            if (mins[i] == 0f || maxes[i] == 0f)
            {
                y = 0f;
            }
            else
            {
                y = Mathf.Lerp(ystart, yend, mins[i] / maxes[i]); //this is just percentage not total actual
            }
            
            points[i] = transform.TransformPoint(new Vector3(x,y,-0.1f));
            
        }
        line.positionCount = mins.Count;
        line.SetPositions(points);
        Debug.Log(mins.Count);
        Debug.Log(line.positionCount);
    }
}
