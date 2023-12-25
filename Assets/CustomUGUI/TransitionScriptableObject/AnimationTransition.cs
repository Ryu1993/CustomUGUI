using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AnimationTransition", menuName = "CustomUGUI/StateTransition/AnimationTransition")]
public class AnimationTransition : StateTransition
{
    [SerializeField] private AnimationTriggers Triggers;
    protected override void Do(TransitionTarget target, SelectionState state, bool instant)
    {
        if (target.TargetAnimator == null || target.TargetAnimator.isActiveAndEnabled == false || target.TargetAnimator.hasBoundPlayables == false) return;
        
        var targetTrigger = state switch {
            SelectionState.Normal => Triggers.normalTrigger,
            SelectionState.Highlighted => Triggers.highlightedTrigger,
            SelectionState.Pressed => Triggers.pressedTrigger,
            SelectionState.Selected => Triggers.selectedTrigger,
            SelectionState.Disabled => Triggers.disabledTrigger,
            _ => string.Empty
        };
        if(string.IsNullOrEmpty(targetTrigger)) return;
        
        target.TargetAnimator.ResetTrigger(Triggers.normalTrigger);
        target.TargetAnimator.ResetTrigger(Triggers.highlightedTrigger);
        target.TargetAnimator.ResetTrigger(Triggers.pressedTrigger);
        target.TargetAnimator.ResetTrigger(Triggers.selectedTrigger);
        target.TargetAnimator.ResetTrigger(Triggers.disabledTrigger);
        
        target.TargetAnimator.SetTrigger(targetTrigger);
    }
}
