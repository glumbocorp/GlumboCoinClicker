using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRenderButton : RenderButton
{
    [SerializeField] Sprite defaultSpriteToggled;
    [SerializeField] Sprite hoverSpriteToggled;
    [SerializeField] Sprite pressedSpriteToggled;
    [SerializeField] InteractorBase toggleInteractor;
    [SerializeField] Color toggleDefaultColor = Color.white;
    [SerializeField] Color toggleHoverColor = Color.white;
    [SerializeField] Color togglePressedColor = Color.white;
    [SerializeField] Color toggleReleasedColor = Color.white;
    [SerializeField] bool toggled = false;

    public override void SetSprite(buttonStates state)
    {
        
        if (!toggled)
        {
            base.SetSprite(state);
        }
        else
        {
            switch (state)
            {
                case buttonStates.HOVER:
                    thisRend.sprite = hoverSpriteToggled; SetColor(toggleHoverColor); break;
                case buttonStates.PRESSED:
                    thisRend.sprite = pressedSpriteToggled; SetColor(togglePressedColor); break;
                case buttonStates.DEFAULT:
                default:
                    thisRend.sprite = defaultSpriteToggled; SetColor(toggleDefaultColor); break;
            }
        }
        
    }

    public void TogglePress()
    {
        if (currentTime == 0f)
        {
            StartCooldown();
            if (forceUpdate) { ForceUpdate(); }
            toggleInteractor.Trigger();
        }
    }

    public override void Press()
    {
        toggled = !toggled;
        if (!toggled)
        {
            base.Press();
        }
        else
        {
            TogglePress();
        }
        
    }
}
