using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogoEspecial : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TMP_Text textoDialogo;
    [SerializeField] private GameObject botonAccionFinal; // Se activa al terminar

    [Header("Diálogo")]
    [SerializeField, TextArea(3, 6)] private string[] lineasDialogo;
    [SerializeField] private float tiempoEntreCaracteres = 0.05f;
    [SerializeField] private int caracteresParaSonido = 3;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip vozNPC;

    private bool jugadorEnRango;
    private bool dialogoActivo;
    private int indexLinea;

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogoActivo)
            {
                IniciarDialogo();
            }
            else if (textoDialogo.text == lineasDialogo[indexLinea])
            {
                SiguienteLinea();
            }
            else
            {
                StopAllCoroutines();
                textoDialogo.text = lineasDialogo[indexLinea];
            }
        }
    }

    private void IniciarDialogo()
    {
        dialogoActivo = true;
        indexLinea = 0;
        panelDialogo.SetActive(true);
        botonAccionFinal?.SetActive(false);

        Time.timeScale = 0f;
        StartCoroutine(MostrarLinea());
    }

    private void SiguienteLinea()
    {
        indexLinea++;
        if (indexLinea < lineasDialogo.Length)
        {
            StartCoroutine(MostrarLinea());
        }
        else
        {
            FinalizarDialogo();
        }
    }

    private void FinalizarDialogo()
    {
        dialogoActivo = false;
        panelDialogo.SetActive(false);
        Time.timeScale = 1f;

        if (botonAccionFinal != null)
        {
            botonAccionFinal.SetActive(true);
            Debug.Log("Botón final activado tras diálogo especial.");
        }
    }

    private IEnumerator MostrarLinea()
    {
        textoDialogo.text = "";
        audioSource.clip = vozNPC;

        int contadorCaracteres = 0;
        foreach (char letra in lineasDialogo[indexLinea])
        {
            textoDialogo.text += letra;
            contadorCaracteres++;

            if (contadorCaracteres % caracteresParaSonido == 0)
            {
                audioSource.Play();
            }

            yield return new WaitForSecondsRealtime(tiempoEntreCaracteres);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            jugadorEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            jugadorEnRango = false;
        }
    }
}
