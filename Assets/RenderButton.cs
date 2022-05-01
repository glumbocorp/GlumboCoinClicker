using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderButton : PhoneElement
{
    public enum buttonStates
    {
        HOVER,
        PRESSED,
        RELEASED,
        DEFAULT
    };
    protected SpriteRenderer thisRend;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite hoverSprite;
    [SerializeField] Sprite pressedSprite;
    [SerializeField] Sprite releasedSprite;
    [SerializeField] InteractorBase interactor;
    [SerializeField] float cooldownTime;//time it takes to reset to default after released
    PhoneClick phoneclick;
    protected float currentTime;
    private void Start()
    {
        thisRend = GetComponent<SpriteRenderer>();
        SetSprite(buttonStates.DEFAULT);
        phoneclick = GameObject.Find("phone").GetComponent<PhoneClick>();
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0f;
                EndCooldown();
            }
        }
    }
    public override void OnHover()
    {
        if (currentTime == 0)
        {
            SetSprite(buttonStates.HOVER);
        }
    }
    public override void StopHover()
    {
        if (currentTime == 0)
        {
            SetSprite(buttonStates.DEFAULT);
        }
    }
    public override void OnHold()
    {
        if (currentTime == 0)
        {
            SetSprite(buttonStates.PRESSED);
        }
    }
    public override void Press()//only trigger when released after hovering
    {
        interactor.Trigger();
        StartCooldown();
    }
    public virtual void StartCooldown()
    {
        currentTime = cooldownTime;
        SetSprite(buttonStates.RELEASED);
    }
    public virtual void EndCooldown()
    {
        
        if(phoneclick.currentHover == gameObject)
        {
            SetSprite(buttonStates.HOVER);
        }
        else
        {
            SetSprite(buttonStates.DEFAULT);
        }
    }
    
    public virtual void SetSprite(buttonStates state)
    {
        switch (state)
        {
            case buttonStates.HOVER:
                thisRend.sprite = hoverSprite; break;
            case buttonStates.PRESSED:
                thisRend.sprite = pressedSprite; break;
            case buttonStates.RELEASED:
                thisRend.sprite = releasedSprite; break;
            case buttonStates.DEFAULT:
            default:
                thisRend.sprite = defaultSprite; break;
        }
    }
}
