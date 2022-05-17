using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageGroup : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text text;
    public Transform bottomTransform;
    [SerializeField] RectTransform textRect;
    // Start is called before the first frame update
    public void ResizeBottom()
    {
        text.ForceMeshUpdate();
        
        bottomTransform.position = textRect.position;
        bottomTransform.localPosition += new Vector3(0f, -text.textBounds.size.y, 0f);
        Debug.Log(bottomTransform.localPosition);
    }
}
