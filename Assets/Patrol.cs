using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : BaseFSM{
    GameObject[] m_WayPoints;
    int m_CurrentWayPoint;

    private void Awake() {
        m_WayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        m_CurrentWayPoint = Random.Range(0, m_WayPoints.Length);
        m_Agent.stoppingDistance = 0.0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        var waypoint = m_WayPoints[m_CurrentWayPoint];
        if(Vector3.Distance(waypoint.transform.position, m_Target.transform.position) < m_Accuracy){
            m_CurrentWayPoint = ++m_CurrentWayPoint % m_WayPoints.Length;
            waypoint = m_WayPoints[m_CurrentWayPoint];
        }

        m_Agent.SetDestination(waypoint.transform.position);
    }
}
