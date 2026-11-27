using UnityEngine;

public class PunchBehavior : StateMachineBehaviour
{
    public enum TPunchType
    {
        LEFT_HAND=0, RIGHT_HAND=1, FOOT=2
    }
    public TPunchType m_PunchType;
    public float m_StartPunchPct;
    public float m_EndPunchPct;
    MarioController m_MarioController;
    public GameObject m_Collider;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MarioController= animator.GetComponent<MarioController>();
        switch (m_PunchType)
        {
            case TPunchType.LEFT_HAND:
                m_Collider= m_MarioController.m_LeftHandCollider;
                break;

            case TPunchType.RIGHT_HAND:
                m_Collider = m_MarioController.m_RightHandCollider;
                break;

            case TPunchType.FOOT:
                m_Collider = m_MarioController.m_KickCollider;
                break;

        }
        m_Collider.SetActive(false);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool l_ColliderActive = stateInfo.normalizedTime >= m_StartPunchPct && stateInfo.normalizedTime <= m_EndPunchPct;
        m_Collider.SetActive(l_ColliderActive);

       
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Collider.SetActive(false);
        m_MarioController.SetIsPunching(false);
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
