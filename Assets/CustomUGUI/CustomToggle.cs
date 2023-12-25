using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CustomToggle : CustomSelectable, IPointerClickHandler, ISubmitHandler
{
    [Header("ToggleSetting")]
    [SerializeField] private bool AllowToggleOff = true;
    [SerializeField] private Graphic ToggleGraphic;
    [SerializeField] private Toggle.ToggleTransition m_ToggleTransition = Toggle.ToggleTransition.None;
    [SerializeField] private CustomToggleGroup m_ToggleGroup;
    [SerializeField] private Toggle.ToggleEvent m_OnValueChanged;
    [SerializeField] private UnityEvent m_OnToggleOn;
    [SerializeField] private UnityEvent m_OnToggleOff;
    [SerializeField] private bool m_IsOn;
    public new bool interactable
    {
        get => base.interactable;
        set
        {
            base.interactable = value;
            if (IsOn)
            {
                ForceSetToggleValue(false);
            }
        }
    }
    
    public bool IsOn
    {
        get => m_IsOn;
        set => SetToggleValue(value);
    }
    public CustomToggleGroup ToggleGroup
    {
        get => m_ToggleGroup;
        set => SetToggleGroup(value);
    }
    public Toggle.ToggleEvent OnValueChanged => m_OnValueChanged ??= new Toggle.ToggleEvent();
    
    private void SetToggleGroup(CustomToggleGroup group)
    {
        m_ToggleGroup = group;
        if (m_ToggleGroup != null)
        {
            m_ToggleGroup.RegisterToggle(this);
        }
    }
    private void SetToggleValue(bool value)
    {
        if (interactable == false) return;
        if(m_IsOn == value) return;
        m_IsOn = value;
        OnValueChanged.Invoke(value);
        if (value)
        {
            m_OnToggleOn?.Invoke();
        }
        else
        {
            m_OnToggleOff?.Invoke();
        }
        if (m_ToggleGroup != null)
        {
            m_ToggleGroup.OnToggleChangedNotify(this);
        }
        DoStateTransition(currentSelectionState,m_ToggleTransition == Toggle.ToggleTransition.None);
    }
    private void ForceSetToggleValue(bool value)
    {
        m_IsOn = value;
        OnValueChanged.Invoke(value);
        if (value)
        {
            m_OnToggleOn?.Invoke();
        }
        else
        {
            m_OnToggleOff?.Invoke();
        }
        if (m_ToggleGroup != null)
        {
            m_ToggleGroup.OnToggleChangedNotify(this);
        }
        DoStateTransition(currentSelectionState,m_ToggleTransition == Toggle.ToggleTransition.None);
    }

    
    protected override SelectionState StateFilter(SelectionState state)
    {
        state = base.StateFilter(state);
        if (state is SelectionState.Normal or SelectionState.Selected)
        {
            state = m_IsOn ? SelectionState.Selected : SelectionState.Normal;
        }
        return state;
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        if(interactable == false) return;
        if(eventData.button != PointerEventData.InputButton.Left) return;
        if (AllowToggleOff)
        {
            IsOn = !IsOn;
        }
        else
        {
            if(IsOn) return;
            IsOn = true;
        }
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if(interactable == false) return;
        if (AllowToggleOff)
        {
            IsOn = !IsOn;
        }
        else
        {
            if(IsOn) return;
            IsOn = true;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (IsOn && interactable)
        {
            ForceSetToggleValue(true);
        }
    }
    
    protected override void OnDestroy()
    {
        if (m_ToggleGroup != null)
        {
            m_ToggleGroup.UnregisterToggle(this);
        }
        base.OnDestroy();
    }


    protected override void OnDidApplyAnimationProperties()
    {
        if (ToggleGraphic != null)
        {
            var oldValue = !Mathf.Approximately(ToggleGraphic.canvasRenderer.GetColor().a, 0);
            if (m_IsOn != oldValue)
            {
                ForceSetToggleValue(oldValue == false);
            }
        }
        base.OnDidApplyAnimationProperties();
    }
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        ForceSetToggleValue(m_IsOn);
        base.OnValidate();
    }
#endif
    
    
}
