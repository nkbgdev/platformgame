
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveForce; 
    private Rigidbody2D rb;
    public bool isGrounded;
     public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private Transform target;

    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if(isGrounded==true)
        {
         rb.AddForce(Vector2.right * moveForce);
        }
    }
}