using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_Body;
    [SerializeField] Animator m_Anim;
    [SerializeField] Enemy m_Enemy;
    [Header("Attack Check")]
    [SerializeField] Transform m_AttackPoint;
    [SerializeField] float m_AttackRadius;
    [SerializeField] LayerMask m_AttackableLayerMask;
    public Transform groundCheckPos;
    public LayerMask groundLayer;

    public Collider2D bodyCollider;
    bool isDectect = false;
    bool mustPatrol = true;
    bool mustTurn;
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        Attack();
    }

    void FixedUpdate() 
    {
        if (mustPatrol)
        {
            mustTurn = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }
    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        transform.Translate((Vector2) transform.right* m_Enemy.MoveSpeed* Time.deltaTime);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y);
        m_Enemy.MoveSpeed *= -1;
        mustPatrol = true;
    }

    void Attack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(m_AttackPoint.position, m_AttackRadius, m_AttackableLayerMask);
        if (cols.Length > 0)
        {
            m_Anim.SetTrigger("attack");
            foreach (Collider2D col in cols)
            {
                if (col.gameObject.TryGetComponent<Character>(out Character character))
                {
                    character.TakeDamage();
                }
            }
        }
    }
    void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(m_AttackPoint.position, m_AttackRadius); 
    }
}
