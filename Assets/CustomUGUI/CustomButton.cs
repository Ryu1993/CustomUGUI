using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CustomButton : CustomSelectable,IPointerClickHandler, ISubmitHandler
{
    [FormerlySerializedAs("onClick")]
    [SerializeField] private Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();
    
    public Button.ButtonClickedEvent OnClick
    {
        get => m_OnClick;
        set => m_OnClick = value;
    }

    private bool IsNotValid => IsActive() == false || IsInteractable() == false;
    
    private void Press()
    {
        if (IsNotValid) return;
        UISystemProfilerApi.AddMarker("CustomButton.OnClick", this);
        m_OnClick.Invoke();
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        Press();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Press();
        if (IsNotValid) return;
        DoStateTransition(SelectionState.Pressed, false);
        StartCoroutine(OnFinishSubmit());
    }
    
    private IEnumerator OnFinishSubmit()
    {
        var fadeTime = colors.fadeDuration;
        var elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        DoStateTransition(currentSelectionState, false);
    }
}
