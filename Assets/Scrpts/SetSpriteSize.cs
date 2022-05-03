using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpriteSize : MonoBehaviour
{
    SpriteRenderer rend;
    [SerializeField] Vector2 size;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Debug.Log(rend.sprite.bounds);
        transform.localScale = size / rend.sprite.bounds.size;
    }
    public void SetSprite(Sprite spr)
    {
        rend.sprite = spr;
        
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.localScale = size / rend.sprite.bounds.size;
    }
}
