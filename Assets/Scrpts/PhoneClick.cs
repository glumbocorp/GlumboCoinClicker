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
    [SerializeField] float scrollSpeed;
    Main main;
    GameObject hovering;
    public GameObject currentHover => hovering;
    PhoneElement hoveringElement;
    Vector2 relativeOffset = Vector2.zero;
    Vector2 lastDisplayPos = Vector2.zero;
    bool holding = false;
    private void Start()
    {
        main = Main.staticMain;
    }
    public void MouseStartDown(Vector3 worldHit)
    {
        if (hovering != null)
        {
            holding = true;
            if(hoveringElement != null)
            {
                relativeOffset = Vector2.zero;
                hoveringElement.OnHold();
                lastDisplayPos = GetDisplayPos(worldHit) ;
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

    Vector2 GetRelativePhone(Vector3 worldHit){
        return new Vector2((worldHit.x - topLeft.position.x) / (botRight.position.x - topLeft.position.x), (worldHit.y - botRight.position.y) / (topLeft.position.y - botRight.position.y));
    }

    Vector3 GetDisplayPos(Vector3 worldHit)
    {
        return convertToDisplay(GetRelativePhone(worldHit));
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
                Action testAction = hoveringElement.GetComponent<Action>();
                if (testAction != null)
                {
                    main.mainClicker.StartHoveringActionFromPhone(hoveringElement.gameObject, testAction.GetText(), worldHit);
                }
                else
                {
                    main.mainClicker.StopHoveringActionFromPhone();
                }
            }
            if(holding){
                Vector2 thisPos = (Vector2)GetDisplayPos(worldHit);
                relativeOffset = thisPos - lastDisplayPos;
                lastDisplayPos = thisPos;
                hoveringElement.OnDrag(relativeOffset);
            }
            if (hovering != null)
            {
                hoveringElement.OnDrag(Input.mouseScrollDelta * scrollSpeed * -1f);
                main.mainClicker.HoveringActionMoveTooltip(worldHit);
            }
        }
        else
        {
            main.mainClicker.StopHoveringActionFromPhone();
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

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))//mouse release
        {
            if (hovering != null && holding)
            {
                MouseFullPress();
            }
            holding = false;
            relativeOffset = Vector2.zero;
            Debug.Log("releasing");
            Debug.Log(holding);
        }
    }
}
