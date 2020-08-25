using UnityEngine;

public class PathNode : MonoBehaviour{
    public Color m_Color = new Color(1.0f, 0.0f, 0.0f, 0.25f);
    public float m_Radius = 0.5f;

    private void OnDrawGizmos() {
        Gizmos.color = m_Color;
        Gizmos.DrawSphere(transform.position, m_Radius);
    }
}