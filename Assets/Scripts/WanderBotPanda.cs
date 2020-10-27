using Panda;
using UnityEngine;
using UnityEngine.AI;

public class WanderBotPanda : MonoBehaviour {
    public float m_Range = 5.0f;
    public float m_FightRadius = 10.0f;
    public float m_Accuracy = 5.0f;
    public LayerMask m_FightLayer;
    [Header("Bullet")]
    public GameObject m_Bullet;
    public Transform m_BulletSpawn;
    public float m_BulletForce = 1000.0f;

    private NavMeshAgent m_Agent;
    private Vector3 m_Target;

    private void Awake() {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    [Task]
    public void PickRandomDestination(){
        var target = transform.position + Random.insideUnitSphere * m_Range;
        target.y = 0.5f;
        m_Agent.SetDestination(target);
        Task.current.Succeed();
    }

    [Task]

    public void PickDestination(float x, float z){
        var target = new Vector3(x, 0.5f, z);
        m_Agent.SetDestination(target);
        Task.current.Succeed();
    }

    [Task]
    public void MoveToDestination(){
        // if (Task.isInspected) Task.current.debugInfo = $"{Time.time:0.00}";
        if (m_Agent.remainingDistance <= m_Agent.stoppingDistance && !m_Agent.pathPending){
            Task.current.Succeed();
        }
    }

    [Task]
    public void TargetPlayer(){
        Collider[] enemies = Physics.OverlapSphere(transform.position, m_FightRadius, m_FightLayer);
        if(enemies != null && enemies.Length > 0){
            m_Target = enemies[0].transform.position;
            Task.current.Succeed();
        }
    }

    [Task]
    public void LookAtTarget(){
        var direction = m_Target - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime);
        if(Vector3.Angle(transform.forward, direction) < m_Accuracy){
            Task.current.Succeed();
        }
    }

    [Task]
    public void Fire(){
        var bullet = Instantiate(m_Bullet, m_BulletSpawn.position, m_BulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * m_BulletForce);
        Task.current.Succeed();
    }

    [Task]
    public void Turn(float angle){
        m_Target = transform.position + Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
        Task.current.Succeed();
    }
    public float m_Health = 100.0f;

    [Task]
    public bool IsHealthLessThan(float health){
        return m_Health < health;
    }
    [Task]
    public bool InDanger(float distance){
        return Vector3.Distance(transform.position, m_Target) < distance;
    }

    [Task]
    public void TakeCover(){
        var awayFromEnemy = transform.position - m_Target;
        var destination = transform.position + awayFromEnemy * 2.0f;
        m_Agent.SetDestination(destination);
        Task.current.Succeed();
    }
}