using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : BaseFSM{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        m_Target.GetComponent<StateMachineBot>().StartFiring();
        m_Agent.stoppingDistance = 3.0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        m_Target.transform.LookAt(m_Opponent.transform.position);
        m_Agent.SetDestination(m_Opponent.transform.position);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        m_Target.GetComponent<StateMachineBot>().StopFiring();
    }
}
