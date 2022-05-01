using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneClick : MonoBehaviour
{
    [SerializeField] Transform topLeft;
    [SerializeField] Transform botRight;
    [SerializeField] Camera phoneCam;
    [SerializeField] Transform displayTopRight;
    [SerializeField] Transform displayBotLeft;
    GameObject hovering;
    public GameObject currentHover => hovering;
    PhoneElement hoveringElement;

    bool holding = false;
    public void OnMouseUp()
    {
        if(hovering!=null && holding)
        {
            MouseFullPress();
        }
        holding = false;
    }
    public void OnMouseDown()
    {
        if (hovering != null)
        {
            holding = true;
            if(hoveringElement != null)
            {
                hoveringElement.OnHold();
            }
        }
    }

    void MouseFullPress()
    {
        if (hovering != null)
        {
            hoveringElement.Press();
        }
    }
    public void MouseClick(Vector3 worldHit)
    {
        //left to right, down to up, starts at 00, ends at 2.58 4.94
        Debug.DrawLine(phoneCam.transform.position, GetDisplayPos(worldHit), Color.red, 4f);
        holding = true;

    }

    Vector3 GetDisplayPos(Vector3 worldHit)
    {
        Vector2 pos = new Vector2((worldHit.x - topLeft.position.x) / (botRight.position.x - topLeft.position.x), (worldHit.y - botRight.position.y) / (topLeft.position.y - botRight.position.y));
        return convertToDisplay(pos);
    }
    public void MouseHover(Vector3 worldHit)
    {
        Vector3 phonePos =  phoneCam.transform.position;
        Vector3 dir;
        dir = (GetDisplayPos(worldHit) - phonePos).normalized * 5f;
        RaycastHit hit;
        Physics.Raycast(phonePos, dir, out hit);
        Debug.DrawRay(phonePos, dir, Color.green, 5f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject != hovering)
            {
                Debug.Log("hovering: " + hit.collider.gameObject.name);
                if (holding)
                {
                    holding = false;
                }
                hovering = hit.collider.gameObject;
                hoveringElement = hovering.GetComponent<PhoneElement>();
                hoveringElement.OnHover();
            }
            
        }
        else
        {
            StopHovering();
            if (holding)
            {
                holding = false;
            }
        }
        

    }
    void StopHovering()
    {
        if (hovering)
        {
            hoveringElement.StopHover();
            hovering = null;
        }
    }
    public Vector3 convertToDisplay(Vector2 inPos)
    {
        Vector3 basePos = displayBotLeft.position;
        float w = displayTopRight.position.x - displayBotLeft.position.x;
        float h = displayTopRight.position.y - displayBotLeft.position.y;
        return basePos + new Vector3(inPos.x * w, inPos.y * h, 0f);
    }
}
