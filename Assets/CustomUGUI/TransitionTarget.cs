using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 인스펙터에서 편집할 경우 해당 StateTransition을 공유하는 모든 곳에서 수정됩니다.
/// </summary>
[System.Serializable]
public class TransitionTarget
{
    [SerializeField] private Graphic m_TargetGraphic;
    [SerializeField] private StateTransition m_Transition;
    private Image m_TargetImage;
    private Animator m_TargetAnimator;
    
    public Graphic TargetGraphic => m_TargetGraphic;
    public Image TargetImage => m_TargetImage ??= m_TargetGraphic as Image;
    public Animator TargetAnimator => m_TargetAnimator ??= m_TargetGraphic == null ? null : m_TargetGraphic.GetComponent<Animator>();
    
    public void DoStateTransition(int state, bool instant)
    {
        if(m_Transition is null) return;
        m_Transition.Do(this,state,instant);
    }
}

