using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpriteSize : MonoBehaviour
{
    SpriteRenderer rend;
    [SerializeField] Vector2 size;
    [SerializeField] Transform comment;
    [SerializeField] float distanceToComment = 0.1f;
    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        


    }
    public void SetSprite(Sprite spr)
    {
        rend.sprite = spr;
        ScaleSprite();
    }
    public void ScaleSprite()
    {
        transform.localScale = size / rend.sprite.bounds.size;
        transform.localPosition = new Vector3(0f, -size.y / 2, 0f);
        if (comment != null)
        {
            comment.localPosition = new Vector3(0f, -size.y - distanceToComment, 0f);
        }
    }
}
