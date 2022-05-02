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
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color hoverColor = Color.white;
    [SerializeField] Color pressedColor = Color.white;
    [SerializeField] Color releasedColor = Color.white;
    [SerializeField] InteractorBase interactor;
    [SerializeField] float cooldownTime;//time it takes to reset to default after released
    PhoneClick phoneclick;
    Color currentColor;
    protected Color lerpColor;
    Color targetColor;
    [SerializeField] float colorTime = 0.2f;
    float currentColorTime;
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
        if (currentColorTime > 0)
        {
            currentColorTime -= Time.deltaTime;
            if(currentColorTime <= 0)
            {
                currentColorTime = 0f;
            }
            thisRend.color = Color.Lerp(targetColor, currentColor,currentColorTime/colorTime);
        }
    }
    public virtual void SetColor(Color col)
    {
        currentColor = thisRend.color;
        currentColorTime = colorTime;
        targetColor = col;
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
        if (currentTime == 0f)
        {
            interactor.Trigger();
            StartCooldown();
        }
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
                thisRend.sprite = hoverSprite; SetColor(hoverColor); break;
            case buttonStates.PRESSED:
                thisRend.sprite = pressedSprite; SetColor(pressedColor); break;
            case buttonStates.RELEASED:
                thisRend.sprite = releasedSprite; SetColor(releasedColor); break;
            case buttonStates.DEFAULT:
            default:
                thisRend.sprite = defaultSprite; SetColor(defaultColor); break;
        }
    }
}
