using UnityEngine;
using System.Collections.Generic;

public class FlockManager : MonoBehaviour{
    [Header("Flocking")]
    public GameObject[] m_BoidPrefabs;
    public int m_NumberBoids = 20;
    public List<GameObject> m_Boids = new List<GameObject>();
    public Vector3 m_Target = Vector3.zero;
    private Collider m_Collider;
    public Bounds Bounds => m_Collider.bounds;

    [Header("Boids")]
    [Range(0.0f, 5.0f)]
    public float m_MinSpeed = 0.4f;
    [Range(0.0f, 5.0f)]
    public float m_MaxSpeed = 1.0f;
    [Range(0.0f, 10.0f)]
    public float m_MinRotationSpeed = 3.0f;
    [Range(0.0f, 10.0f)]
    public float m_MaxRotationSpeed = 6.0f;
    [Range(0.0f, 10.0f)]
    public float m_NeighbourDistance = 0.5f;

    private void Start(){
        m_Collider = GetComponent<Collider>();
        m_Collider.isTrigger = true;
        var limits = m_Collider.bounds;
        for (int i = 0; i < m_NumberBoids; i++)
        {
            var position = transform.position;
            position.x += Random.Range(limits.min.x, limits.max.x);
            position.y += Random.Range(limits.min.y, limits.max.y);
            position.z += Random.Range(limits.min.z, limits.max.z);


            var index = Random.Range(0, m_BoidPrefabs.Length);
            var boid = Instantiate(m_BoidPrefabs[index], position, Quaternion.identity, transform);
            boid.GetComponent<Boid>().m_Manager = this;
            m_Boids.Add(boid);
        }

        m_Target = transform.position;
    }

    private void Update() {
        if(Random.Range(0.0f, 100.0f) > 10.0f) return;
        var limits = m_Collider.bounds;

        var position = transform.position;
        position.x += Random.Range(limits.min.x, limits.max.x);
        position.y += Random.Range(limits.min.y, limits.max.y);
        position.z += Random.Range(limits.min.z, limits.max.z);

        m_Target += position;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_Target, 0.5f);    
    }
}