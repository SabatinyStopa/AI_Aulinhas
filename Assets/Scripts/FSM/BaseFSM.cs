using UnityEngine;
using UnityEngine.AI;

public class BaseFSM: StateMachineBehaviour {
    public float m_Speed = 2.0f;
    public float m_Accuracy = 1.0f;
    public GameObject m_Target;
    public NavMeshAgent m_Agent;
    public GameObject m_Opponent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        m_Target = animator.gameObject;
        m_Agent = m_Target.GetComponent<NavMeshAgent>();
        m_Opponent = m_Target.GetComponent<StateMachineBot>().m_Player;
    }
}