using UnityEngine;

public abstract class StateTransition : ScriptableObject
{
    protected enum SelectionState
    {
        Normal,
        Highlighted,
        Pressed,
        Selected,
        Disabled,
    }

    public void Do(TransitionTarget target, int state, bool instant)
    {
        if(target?.TargetGraphic is null) return;
        if(target.TargetGraphic.gameObject.activeInHierarchy == false) return;
        Do(target,(SelectionState)state,instant);
    }
    protected abstract void Do(TransitionTarget target, SelectionState state, bool instant);
}
