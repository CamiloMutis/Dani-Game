using UnityEngine;

public class EventoNPCConBotonQuit : MonoBehaviour
{
    [SerializeField] private GameObject botonQuit;
    [SerializeField] private GameObject jugador;

    private bool ejecutado = false;

    public void ActivarEvento()
    {
        if (ejecutado) return;
        ejecutado = true;

        if (jugador != null)
        {
            // 1. Detener movimiento físico
            var rb = jugador.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }

            // 2. Detener animación de caminar
            var animator = jugador.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetFloat("Speed", 0); // Cambia "Speed" si tu parámetro tiene otro nombre
            }

            // 3. Desactivar script de movimiento del jugador
            var control = jugador.GetComponent<PlayerControls>(); // Cambia por el nombre de tu script de movimiento real
            if (control != null)
            {
                control.enabled = false;
            }
        }

        // 4. Activar el botón Quit
        if (botonQuit != null)
        {
            botonQuit.SetActive(true);
        }
    }

    // OPCIONAL: Si quieres reactivar el control al cerrar el diálogo o pulsar "Quit"
    public void ReactivarJugador()
    {
        if (jugador != null)
        {
            var control = jugador.GetComponent<PlayerControls>();
            if (control != null)
            {
                control.enabled = true;
            }
        }

        if (botonQuit != null)
        {
            botonQuit.SetActive(false);
        }
    }
}
