using UnityEngine;

public class Player : MonoBehaviour {
    public float m_SpeedMove = 10.0f;
    public float m_SpeedRotate = 180;
    private void Update() {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up, horizontal * m_SpeedRotate * Time.deltaTime);
        transform.Translate(Vector3.forward * vertical * m_SpeedMove * Time.deltaTime);
    }
}