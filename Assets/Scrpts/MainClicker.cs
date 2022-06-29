using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainClicker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera mainCam;
    [SerializeField] GlumboCoin coin;
    [SerializeField] PhoneClick phone;
    [SerializeField] Tooltip tooltip;
    GameObject hoveringAction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray camRay = mainCam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            RaycastHit hitRay;
            Physics.Raycast(camRay, out hitRay);
            if (hitRay.collider != null)
            {
                switch (hitRay.collider.tag)
                {
                    case "Glumbocoin":
                        coin.Spin();
                        break;
                    case "Phone":
                        phone.MouseStartDown(hitRay.point);
                        break;
                    case "Action":
                        hitRay.collider.GetComponent<Action>().StartAction();
                        break;
                    default: break;
                }
            }
        }
        else
        {
            Ray camRay = mainCam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            RaycastHit hitRay;
            Physics.Raycast(camRay, out hitRay);
            if (hitRay.collider != null)
            {
                switch (hitRay.collider.tag)
                {
                    case "Glumbocoin":
                        //glow the coin?
                        break;
                    case "Phone":
                        phone.MouseHover(hitRay.point);
                        break;
                    case "Action":
                        if (hoveringAction == null || hoveringAction != hitRay.collider.gameObject)
                        {
                            hoveringAction = hitRay.collider.gameObject;
                            tooltip.Active(true);
                            tooltip.SetText(hitRay.collider.gameObject.GetComponent<Action>().GetText());
                            tooltip.transform.position = hitRay.point;
                        }
                        else
                        {
                            tooltip.transform.position = hitRay.point;
                        }
                        break;
                    default: break;
                }
            }
            if (hitRay.collider == null)
            {
                tooltip.Active(false);
                hoveringAction = null;
            }
        }
    }

    public void StartHoveringActionFromPhone(GameObject actionObj, string text, Vector3 hitPoint)
    {
        hoveringAction = actionObj;
        tooltip.Active(true);
        tooltip.SetText(text);
        tooltip.transform.position = hitPoint;
    }
    public void HoveringActionMoveTooltip(Vector3 hitPoint)
    {
        tooltip.transform.position = hitPoint;
    }
    public void StopHoveringActionFromPhone()
    {
        tooltip.Active(false);
        hoveringAction = null;
    }
}
