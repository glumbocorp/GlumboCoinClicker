using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRenderButton : RenderButton
{
    [SerializeField] Sprite defaultSpriteToggled;
    [SerializeField] Sprite hoverSpriteToggled;
    [SerializeField] Sprite pressedSpriteToggled;
    [SerializeField] Sprite releasedSpriteToggled;
    [SerializeField] InteractorBase toggleInteractor;
    [SerializeField] bool toggled = false;
    public override void Press()
    {
        base.Press();
        toggled = !toggled;
    }

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
                    thisRend.sprite = hoverSpriteToggled; break;
                case buttonStates.PRESSED:
                    thisRend.sprite = pressedSpriteToggled; break;
                case buttonStates.RELEASED:
                    thisRend.sprite = releasedSpriteToggled; break;
                case buttonStates.DEFAULT:
                default:
                    thisRend.sprite = defaultSpriteToggled; break;
            }
        }
        
    }

}
