using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour {
    private NavMeshAgent m_Agent;
    [Header("Target")]
    public Transform m_Target;
    
    [Header("Wander")]
    public Vector3 m_WanderTarget = Vector3.zero;
    public float m_WanderRadius = 10.0f;
    public float m_WanderDistance = 20.0f;
    public float m_WanderJitter = 1.0f;
    [Header("Hiding")]
    private GameObject[] m_HidingSpots;
    public float m_HidingOffset = 3.0f;

    [Header("Path follow")]
    public Path0 m_Path;

    public bool seek = false;
    public bool wander = false;
    public bool hide = false;

    private Animator m_Animator;
    private void Awake() {
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
       
        m_HidingSpots = GameObject.FindGameObjectsWithTag("HidingSpot");
    }

    private void PathFollow(){
        var node = m_Path.GetNode();
        Seek(node);

        var radius = m_Path.GetRadius();

        if(Vector3.Distance(transform.position, node) < radius){
            m_Path.NextNode();
        }
    }

    private void Hide(){
        float chosenSpotDistance = Mathf.Infinity;
        Vector3 chosenSpotPosition = Vector3.zero;

        var positions = new List<Vector3>();
        foreach (var hidingSpot in m_HidingSpots){
            var direction = hidingSpot.transform.position - m_Target.transform.position;
            var position = hidingSpot.transform.position + direction.normalized * m_HidingOffset;
            var distance = Vector3.Distance(transform.position, position);

            if(distance < chosenSpotDistance){
                chosenSpotDistance = distance;
                chosenSpotPosition = position;
            }
        }

        Seek(chosenSpotPosition);
    }

    private void Seek(Vector3 position){
        m_Agent.SetDestination(position);
    }

    private void Flee(Vector3 position){
        var fleeVector = position - transform.position;
        m_Agent.SetDestination(transform.position - position);
    }

    private float WanderRandom => Random.Range(-1.0f, 1.0f) * m_WanderJitter;

    private void Wander(){
        m_WanderTarget += new Vector3(WanderRandom, 0, WanderRandom);
        
        m_WanderTarget.Normalize();
        m_WanderTarget *= m_WanderRadius;

        var targetLocal = m_WanderTarget + new Vector3(0, 0, m_WanderDistance);
        var targetWorld = gameObject.transform.InverseTransformVector(targetLocal);

        Seek(transform.position + targetWorld);
    }

    private void Update(){
        m_Animator.SetFloat("Speed", m_Agent.velocity.magnitude);

        PathFollow();
        // if(hide)
        //     Hide();

        // if(wander)
        //     Wander();
        // else{
        //     if(seek)
        //         Seek(m_Target.position);
        //     else
        //         Flee(m_Target.position);
        // }
    }   
}