using UnityEngine;

public class StateMachineBot : MonoBehaviour {
    
    public Animator m_State;
    public GameObject m_Player;
    public GameObject m_Bullet;
    public float m_FireRate = 0.5f;
    public Transform m_FirePoint;
    public float m_FireForce = 500.0f;
    [Range(0.0f, 100.0f)]
    public float m_Life = 100.0f;
    public void StartFiring(){
        InvokeRepeating("Fire", 0.0f, m_FireRate);
    }

    public void StopFiring(){
        CancelInvoke("Fire");
    }

    private void Fire(){
        var bullet = Instantiate(m_Bullet, m_FirePoint.position, m_FirePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(m_FirePoint.transform.forward * m_FireForce);

    }

    private void Update(){
        m_State.SetFloat("Life", m_Life);

        if(m_Player){
            var distance = Vector3.Distance(transform.position, m_Player.transform.position);
            m_State.SetFloat("Distance", distance);
        }else
            m_State.SetFloat("Distance", float.MaxValue);     
    }
}