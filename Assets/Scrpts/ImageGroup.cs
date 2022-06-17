using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageGroup : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text text;
    public Transform bottomTransform;
    [SerializeField] RectTransform textRect;
    [SerializeField] float bottomPadding;
    // Start is called before the first frame update
    public void SetComment(string name, string content)
    {
        text.text = "<color=#191919><b>" +name + "</b></color>\n"+ content;
        ResizeBottom();
    }
    public void ResizeBottom()
    {
        text.ForceMeshUpdate();
        
        bottomTransform.position = textRect.position;
        bottomTransform.localPosition += new Vector3(0f, -text.textBounds.size.y - bottomPadding, 0f);
    }
}
