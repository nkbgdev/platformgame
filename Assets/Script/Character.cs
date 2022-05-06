using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float m_Health;
    [SerializeField] float m_MoveSpeed;
    [SerializeField] float m_JumpForce;

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

    public float JumpForce
    {
        get {return m_JumpForce;}
        set {m_JumpForce = value;}
    }

    public void TakeDamage()
    {
        m_Health-=10;
        print(m_Health);
    }
}
