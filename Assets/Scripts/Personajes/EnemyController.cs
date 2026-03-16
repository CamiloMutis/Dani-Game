using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    public float fuerzaRebote = 10f;
    public int vida = 3;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enMovimiento;
    private bool recibiendoDanio;
    private bool playerVivo;
    private bool muerto;
    private Animator animator;

    void Start()
    {
        playerVivo = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerVivo && !muerto)
        {
            Movimiento();
        }
        animator.SetBool("enMovimiento", enMovimiento);
        animator.SetBool("muerto", muerto);
    }

    private void Movimiento()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // Cambia la escala para mirar al jugador
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            movement = new Vector2(direction.x, 0);
            enMovimiento = true;
        }
        else
        {
            movement = Vector2.zero;
            enMovimiento = false;
        }

        // Solo se mueve si no está recibiendo daño
        if (!recibiendoDanio)
        {
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            PlayerControls playerScript = collision.gameObject.GetComponent<PlayerControls>();


            playerScript.RecibirDanio(direccionDanio, 1);
            playerVivo = !playerScript.muerto;
            if (!playerVivo)
            {
                enMovimiento = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Arma1"))
    {
        Vector2 direccionDanio = collision.transform.position;
        RecibeDanio(direccionDanio,1);
    }
}


    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if (!recibiendoDanio)
        {
            vida -= cantDanio;
            recibiendoDanio = true;

            if (vida <= 0)
            {
                muerto = true;
                enMovimiento = false;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;

                StartCoroutine(FadeAndDestroy());
            }
            else
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.5f).normalized;

                rb.linearVelocity = Vector2.zero;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);

                Invoke("ReiniciarDanio", 0.5f);
            }
        }
    }

// Añadir esto dentro de EnemyController

private IEnumerator FadeAndDestroy()
{
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    Color original = sr.color;

    for (float a = 1; a >= 0; a -= 0.02f)
    {
        sr.color = new Color(original.r, original.g, original.b, a);
        yield return new WaitForSeconds(0.1f);
    }

    DeleteBody();
}

public void DeleteBody()
{
    Destroy(gameObject);
}

    private void ReiniciarDanio()
    {
        recibiendoDanio = false;
    }
    void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, detectionRadius);
}

}
