using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour{
    public FlockManager m_Manager;

    public float m_Speed;
    private bool m_Turning;

    private void Start() {
        m_Speed = Random.Range(m_Manager.m_MinSpeed, m_Manager.m_MaxSpeed);
    }

    private void Update() {
        var bounds = m_Manager.Bounds;
        var hit = new RaycastHit();
        var direction = Vector3.zero;

        if(!bounds.Contains(transform.position)){
            m_Turning = true;
            direction = m_Manager.Bounds.center - transform.position;
        }else if(Physics.Raycast(transform.position, transform.forward * 10.0f, out hit)){
            m_Turning = true;
            direction = Vector3.Reflect(transform.forward, hit.normal);
        }else{
            m_Turning = false;
        }

        if(m_Turning){
            BackHome(direction);
        }else{
            ChangeSpeed();
            ApplyRules();
        }

        transform.Translate(0, 0, m_Speed * Time.deltaTime);
    }

    private void BackHome(Vector3 direction){
        var rotation = Random.Range(m_Manager.m_MinRotationSpeed, m_Manager.m_MaxSpeed);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                rotation * Time.deltaTime
            );
    }

    private void ChangeSpeed(){
        if(Random.Range(0.0f, 100.0f) > 10.0f) return;
        m_Speed = Random.Range(m_Manager.m_MinSpeed, m_Manager.m_MaxSpeed);
    }

    private void ApplyRules(){
        if(Random.Range(0.0f, 100.0f) > 20.0f) return;

        var cohesion = Vector3.zero;
        var separation = Vector3.zero;
        var groupSize = 0;
        var distance = 0.0f;
        var speed = 0.01f;

        foreach (var boid in m_Manager.m_Boids){
            if(boid == gameObject) continue;

            distance = Vector3.Distance(transform.position, boid.transform.position);
            if(distance <= m_Manager.m_NeighbourDistance) continue;

            groupSize++;
            cohesion += boid.transform.position;

            if(distance < 1.0f){
                separation += transform.position - boid.transform.position;
            }

            speed += boid.GetComponent<Boid>().m_Speed;
        }

        if(groupSize == 0) return;

        cohesion = cohesion / groupSize + (m_Manager.m_Target - transform.position);
        m_Speed = speed / groupSize;

        var direction = (cohesion + separation) - transform.position;
        if(direction == Vector3.zero) return;

        var rotationSpeed = Random.Range(m_Manager.m_MinRotationSpeed, m_Manager.m_MaxSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
    }
}
