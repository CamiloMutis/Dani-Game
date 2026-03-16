using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    public PlayerSoundController playerSoundController;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float reboundForce = 10f;
    public LayerMask capaSuelo;
    public int saltosMax;
    public float pasoIntervalo = 0.35f;
    public int vida = 3;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private int saltosRestantes;
    private Animator animator;
    private Coroutine pasosCoroutine;
    private bool estaCorriendo = false;
    private bool recibiendoDanio;
    private bool atacando;
    public bool muerto;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        saltosRestantes = saltosMax;
    }

    void Update()
    {
        // SIEMPRE actualiza el parámetro de muerte
        animator.SetBool("muerto", muerto);

        if (!muerto)
        {
            if (!atacando)
            {
                ProcesarMovimiento();
                ProcesarSalto();

                if (EstaEnSuelo() && rb.linearVelocity.y <= 0.01f)
                {
                    animator.SetBool("isJumping", false);
                }

                if (Input.GetMouseButtonDown(0) && EstaEnSuelo())
                {
                    Atacando();
                }
            }

            animator.SetBool("recibeGolpe", recibiendoDanio);
            animator.SetBool("Atacando", atacando);
        }
    }



    public void RecibirDanio(Vector2 direccion, int cantDanio)
    {
        recibiendoDanio = true;
        vida -= cantDanio;

        if (vida <= 0)
        {
            muerto = true;
        }
        if (!muerto)
        {
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            rb.AddForce(rebote * reboundForce, ForceMode2D.Impulse);
        }


        animator.SetBool("isRunning", false);       // Detiene la animación de correr
        animator.SetTrigger("recibeGolpe");         // Activa la animación de daño

        if (playerSoundController != null)
        {
            playerSoundController.playGolpe();      // Reproduce sonido de golpe 🎧
        }

        Invoke("DesactivaDanio", 0.4f);             // Recupera control automáticamente
    }


    public void DesactivaDanio()
    {
        recibiendoDanio = false;
    }

    public void Atacando()
    {
        atacando = true;
        playerSoundController.playAtaque();
    }

    public void DesactivarAtaque()
    {
        atacando = false;
    }

    bool EstaEnSuelo()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.1f,
            capaSuelo
        );
        return hit.collider != null;
    }

    void ProcesarMovimiento()
    {
        if (recibiendoDanio) return; // Solo se bloquea si estás recibiendo daño

        float horizontal = Input.GetAxis("Horizontal");

        // Girar sprite según dirección
        Vector3 scale = transform.localScale;
        if (horizontal < 0) scale.x = -1;
        else if (horizontal > 0) scale.x = 1;
        transform.localScale = scale;

        // Movimiento
        transform.position += new Vector3(horizontal, 0f, 0f) * Time.deltaTime * moveSpeed;

        // Animación de correr
        animator.SetBool("isRunning", horizontal != 0);

        // Sonido de pasos
        bool debeCorrer = horizontal != 0 && EstaEnSuelo();
        if (debeCorrer && !estaCorriendo)
        {
            estaCorriendo = true;
            pasosCoroutine = StartCoroutine(RepetirSonidoPasos());
        }
        else if (!debeCorrer && estaCorriendo)
        {
            estaCorriendo = false;
            if (pasosCoroutine != null) StopCoroutine(pasosCoroutine);
        }
    }


    IEnumerator RepetirSonidoPasos()
    {
        while (true)
        {
            // Solo reproduce el sonido si está corriendo y no está atacando
            if (estaCorriendo && !atacando && vida > 0)
            {
                playerSoundController.playCorrer();
            }

            yield return new WaitForSeconds(pasoIntervalo);
        }
    }


    void ProcesarSalto()
    {
        if (recibiendoDanio) return; // Evita saltar si estás recibiendo daño

        if (EstaEnSuelo() && rb.linearVelocity.y <= 0.01f)
        {
            saltosRestantes = saltosMax;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes--;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            playerSoundController.playSaltar();
        }
    }
}
