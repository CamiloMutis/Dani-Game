using UnityEngine;

public class ZonaMortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControls jugador = other.GetComponent<PlayerControls>();
            if (jugador != null)
            {
                jugador.vida = 0;             // Le bajamos la vida directamente
                jugador.muerto = true;        // Activamos la muerte

                // Bloqueamos movimiento
                Rigidbody2D rb = jugador.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.gravityScale = 0f;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }

                // Activamos animación de muerte
                Animator anim = jugador.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.SetBool("muerto", true);
                }

            }
        }
    }
}
