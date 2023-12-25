using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "SpriteTransition", menuName = "CustomUGUI/StateTransition/SpriteTransition")]
public class SpriteTransition : StateTransition
{
    [SerializeField] private SpriteState Sprites;
    protected override void Do(TransitionTarget target, SelectionState state, bool instant)
    {
        if(target.TargetImage == null) return;
        target.TargetImage.overrideSprite = state switch
        {
            SelectionState.Normal => null,
            SelectionState.Highlighted => Sprites.highlightedSprite,
            SelectionState.Pressed => Sprites.pressedSprite,
            SelectionState.Selected => Sprites.selectedSprite,
            SelectionState.Disabled => Sprites.disabledSprite,
            _ => null
        };
    }
}
