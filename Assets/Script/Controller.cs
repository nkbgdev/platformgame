using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_Body;
    [SerializeField] Animator m_Anim;
    [SerializeField] Character m_Character;
    [Header("Ground Check")]
    [SerializeField] Transform m_GroundCheck;
    [SerializeField] float m_GroundCheckRadius;
    [SerializeField] LayerMask m_JumpableLayerMask;
    [Header("Attack Check")]
    [SerializeField] Transform m_AttackPoint;
    [SerializeField] float m_AttackRadius;
    [SerializeField] LayerMask m_AttackableLayerMask;
    private float dirX = 0f;
    bool m_isGrounded = true;

    void Update()
    {
        Idle();
        Run();
        Jump();
        Fall();
        Attack();
        GroundCheck();
    }

    void Idle()
    {

    }
    
    void Run()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        Flip();
        transform.Translate((Vector2) transform.right* dirX* m_Character.MoveSpeed* Time.deltaTime);
        if (m_isGrounded) m_Anim.SetFloat("speed", Mathf.Abs(dirX));
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_Body.velocity = Vector2.up * m_Character.JumpForce;
        }
        if (m_Body.velocity.y > .1f ) m_Anim.SetBool("jump", true);
    }

    void Fall()
    {
        if (m_Body.velocity.y < -.1f )
        {
            m_Anim.SetBool("jump", false);
            m_Anim.SetBool("fall", true);
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            m_Anim.SetTrigger("attack");
            Collider2D[] cols = Physics2D.OverlapCircleAll(m_AttackPoint.position, m_AttackRadius, m_AttackableLayerMask);
            foreach (Collider2D col in cols)
            {
                if (col.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.TakeDamage();
                }
            }
        }
    }
    
    void Flip()
    {
        if (dirX != 0)
        {
            transform.localScale = new Vector3(-dirX, 1, 1);
        }
    }

    void GroundCheck()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundCheckRadius, m_JumpableLayerMask);
        m_isGrounded = cols.Length > 0;
        if (m_isGrounded) m_Anim.SetBool("fall", false);
    }

    void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(m_GroundCheck.position, m_GroundCheckRadius);
        Gizmos.DrawWireSphere(m_AttackPoint.position, m_AttackRadius); 
    }
}
