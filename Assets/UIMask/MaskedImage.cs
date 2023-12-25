using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MaskedImage : Image
{
    [SerializeField] private Graphic m_MaskGraphic;
    
    public override bool Raycast(Vector2 sp, Camera eventCamera)
    {
        if (base.Raycast(sp, eventCamera) == false) return false;
        return m_MaskGraphic == null || RectTransformUtility.RectangleContainsScreenPoint(m_MaskGraphic.rectTransform, sp, eventCamera) == false;
    }
}
