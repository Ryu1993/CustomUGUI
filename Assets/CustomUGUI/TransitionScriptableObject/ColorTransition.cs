using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ColorTransition", menuName = "CustomUGUI/StateTransition/ColorTransition")]
public class ColorTransition : StateTransition
{
    [SerializeField] private ColorBlock Colors = ColorBlock.defaultColorBlock;
    
    protected override void Do(TransitionTarget target, SelectionState state, bool instant)
    {
        Color targetColor = state switch
        {
            SelectionState.Normal => Colors.normalColor,
            SelectionState.Highlighted => Colors.highlightedColor,
            SelectionState.Pressed => Colors.pressedColor,
            SelectionState.Selected => Colors.selectedColor,
            SelectionState.Disabled => Colors.disabledColor,
            _ => Color.black
        };
        target.TargetGraphic.CrossFadeColor(targetColor, instant ? 0f : Colors.fadeDuration, true, true);
    }
}
