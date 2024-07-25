using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneScroll : PhoneElement
{
    [SerializeField] Vector2 inputMultiplier;
    Vector2 targetOffset = Vector2.zero;
    [SerializeField] Vector2 currentOffset = Vector2.zero;
    [SerializeField] Transform scrollview;
    [SerializeField] bool hasMin;
    [SerializeField] bool hasMax;
    [SerializeField] Vector2 minOffset;
    [SerializeField] Vector2 maxOffset;

    void Start(){
        targetOffset = scrollview.transform.position;
        currentOffset = scrollview.transform.position;
    }
    public override void OnDrag(Vector2 relativeOffset)
    {
        targetOffset += relativeOffset * inputMultiplier;
        if(hasMin) targetOffset = new Vector2(Mathf.Max(minOffset.x, targetOffset.x), Mathf.Max(minOffset.y, targetOffset.y));
        if(hasMax) targetOffset = new Vector2(Mathf.Min(minOffset.x, targetOffset.x), Mathf.Min(minOffset.y, targetOffset.y));

    }

    void UpdateOffset(){
        scrollview.transform.position = new Vector3(currentOffset.x,currentOffset.y, scrollview.transform.position.z);
    }

    void Update(){
        currentOffset = Vector2.Lerp(currentOffset,targetOffset,0.1f);
        if(Vector2.Distance(currentOffset,targetOffset)<0.01f && currentOffset!=targetOffset){
            currentOffset = targetOffset;
            UpdateOffset();
        }else{
            UpdateOffset();
        }
    }
}
