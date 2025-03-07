using UnityEngine;

namespace Enemy
{

public class EnemyAI : MonoBehaviour
{
    public float speed = 0.1f;
    public float patrolDistance = 0.2f;
    public float changeDirectionInterval = 0.6f;
    public float moveInterval = 0.1f;
    public float idleTime = 10.0f; // Tiempo de pausa entre movimientos
    
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private int direction = -1;
    private float changeDirectionTimer;
    private float moveTimer;
    private float idleTimer;
    private bool isIdle = false; // Indica si el enemigo está en pausa
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        startPosition = transform.position;
        changeDirectionTimer = changeDirectionInterval;
        moveTimer = moveInterval;
        idleTimer = idleTime;
    }

    void FixedUpdate()
    {
        if (isIdle)
        {
            idleTimer -= Time.fixedDeltaTime;
            rb.linearVelocity = Vector2.zero; // ✅ Mantiene al enemigo quieto
            animator.SetFloat("Speed", 0); // ✅ Asegura que la animación sea "idle"

            if (idleTimer <= 0)
            {
                isIdle = false; // Vuelve a moverse
                moveTimer = moveInterval;
                idleTimer = idleTime;
            }
            return; // Sale de la función sin moverse
        }

        moveTimer -= Time.fixedDeltaTime;

        if (moveTimer <= 0) // Movimiento normal
        {
            rb.linearVelocity = new Vector2(speed * direction, rb.linearVelocity.y); // ✅ Usa rb.velocity en lugar de rb.linearVelocity

            animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

            if (Mathf.Abs(rb.linearVelocity.x) > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Moviendo"))
            {
                animator.SetTrigger("moviendo");
            }

            changeDirectionTimer -= Time.fixedDeltaTime;
            if (changeDirectionTimer <= 0)
            {
                if (Mathf.Abs(transform.position.x - startPosition.x) >= patrolDistance)
                {
                    ChangeDirection();
                    startPosition = transform.position;
                }
                changeDirectionTimer = changeDirectionInterval;
                
                // Se detiene cada vez que cambia de dirección
                isIdle = true;
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            ChangeDirection();
            isIdle = true; // Se detiene al chocar con una pared
        }
    }

    void ChangeDirection()
    {
        direction *= -1;
        Flip();
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
}
