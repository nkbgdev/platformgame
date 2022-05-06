using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float m_Health;
    [SerializeField] float m_MoveSpeed;
    public float Health
    {
        get {return m_Health;}
        set {m_Health = value;}
    }

    public float MoveSpeed
    {
        get {return m_MoveSpeed;}
        set {m_MoveSpeed = value;}
    }
    public void TakeDamage()
    {
        print("damn");
    }
}
