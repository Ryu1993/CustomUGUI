using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomToggleGroup : MonoBehaviour
{
    [SerializeField] private bool IsReduplication;
    [SerializeField] private List<CustomToggle> Toggles = new List<CustomToggle>();
    private CustomToggle CurActivateToggle;


#if UNITY_EDITOR
    private void OnValidate()
    {
        if(Toggles == null || Toggles.Count == 0) return;
        foreach (var toggle in Toggles.Where(toggle => toggle.ToggleGroup != this))
        {
            if(toggle == null) continue;
            toggle.ToggleGroup = this;
        }
    }
#endif
    
    public void RegisterToggle(CustomToggle toggle)
    {
        if(Toggles.Contains(toggle)) return;
        Toggles.Add(toggle);
        if (IsReduplication || toggle.IsOn == false || CurActivateToggle == toggle) return;
        if (CurActivateToggle != null)
        {
            CurActivateToggle.IsOn = false;
        }
        CurActivateToggle = toggle;
    }

    public void UnregisterToggle(CustomToggle toggle)
    {
        if(Toggles.Contains(toggle) == false) return;
        Toggles.Remove(toggle);
        if (CurActivateToggle == toggle)
        {
            CurActivateToggle = null;
        }
    }
    
    public void OnToggleChangedNotify(CustomToggle toggle)
    {
        if (IsReduplication) return;
        if (Toggles.Contains(toggle) == false) return;
        if (CurActivateToggle == toggle && toggle.IsOn == false)
        {
            CurActivateToggle = null;
        }
        if (CurActivateToggle == toggle && toggle.IsOn) return;
        if (CurActivateToggle != null)
        {
            CurActivateToggle.IsOn = false;
        }
        CurActivateToggle = toggle;
    }
    
}
