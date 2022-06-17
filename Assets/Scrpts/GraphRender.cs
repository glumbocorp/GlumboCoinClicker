using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphRender : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    List<float> mins;
    List<float> maxes;
    List<float> times;
    [SerializeField] float maxTrackingTime;

    private void Awake()
    {
        mins = new List<float>();
        maxes = new List<float>();
        times = new List<float>();
    }
    private void Update()
    {
        for(int i = 0; i < times.Count; i++)
        {
            times[i] += Time.deltaTime;
            if (times[i] > maxTrackingTime)
            {
                mins.RemoveAt(i);
                maxes.RemoveAt(i);
                times.RemoveAt(i);
                i--;
            }
        }
        Display();
    }

    private void Display()
    {
        if (mins.Count <= 0)
        {
            return;
        }
        float xstart = -.5f;
        float xend = .5f;
        float ystart = -.5f;
        float yend = .5f;
        Vector3[] points;
        points = new Vector3[mins.Count];
        for (int i = 0; i < mins.Count; i++)
        {
            float x = Mathf.Lerp(xstart, xend, times[i] / (float)maxTrackingTime);
            float y;
            if (mins[i] == 0f || maxes[i] == 0f)
            {
                y = 0f;
            }
            else
            {
                y = Mathf.Lerp(ystart, yend, mins[i] / maxes[i]); //this is just percentage not total actual
            }

            points[i] = transform.TransformPoint(new Vector3(x, y, -0.1f));

        }
        float firstY = 0f;
        if (mins[mins.Count - 1] == 0f || maxes[mins.Count - 1] == 0f)
        {
            firstY = 0f;
        }
        else
        {
            firstY = Mathf.Lerp(ystart, yend, mins[mins.Count - 1] / maxes[mins.Count - 1]); //this is just percentage not total actual
        }
        if (points.Length > 0)
        {
            points[mins.Count-1] = transform.TransformPoint(new Vector3(xstart, firstY, -0.1f));

            line.positionCount = mins.Count;
            line.SetPositions(points);
        }
        
    }

    public void AddNew(float min, float max)//current and total users
    {
        mins.Add(min);
        maxes.Add(max);
        times.Add(0f);
    }
}
