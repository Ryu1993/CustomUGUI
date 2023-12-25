using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class CustomSelectable : Selectable
{
    [SerializeField] private List<TransitionTarget> SelectableTargets;
    public bool IsPress { get; private set; }
    public bool IsHover { get; private set; }
    public bool IsSelect { get; private set; }
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        state = StateFilter(state);
        base.DoStateTransition(state, instant);
        if(SelectableTargets == null) return;
        foreach (var target in SelectableTargets)
        {
            target.DoStateTransition((int)state,instant);
        }
        
    }
    
    protected virtual SelectionState StateFilter(SelectionState state)
    {
        return state;
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        IsPress = true;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        IsPress = false;
        base.OnPointerUp(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        IsHover = true;
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        IsHover = false;
        base.OnPointerExit(eventData);
        if (IsSelect == false) return;
        if (EventSystem.current == null || EventSystem.current.alreadySelecting || EventSystem.current.currentSelectedGameObject != gameObject)
            return;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        IsSelect = true;
        base.OnSelect(eventData);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        IsSelect = false;
        base.OnDeselect(eventData);
    }
    
}