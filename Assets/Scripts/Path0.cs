using UnityEngine;

[System.Serializable]
public class Path0{
    public PathNode[] m_Nodes;
    private int m_CurrentIndex = 0;

    public float GetRadius(){
        return m_Nodes[m_CurrentIndex].m_Radius;
    }
    public Vector3 GetNode(){
        return m_Nodes[m_CurrentIndex].transform.position;
    }

    public void NextNode(){
        m_CurrentIndex = ++m_CurrentIndex % m_Nodes.Length;          
    }
}