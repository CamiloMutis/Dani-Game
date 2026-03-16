using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuInicial : MonoBehaviour
{
    void Start()
    {
        // Vacío por ahora
    }

    public void PlayGame()
{
    PersistenteAudio.instancia?.DetenerMusica(); // 🔇 Detenemos la música justo antes del cambio
    StartCoroutine(EsperarYCargarEscena("GameDani"));
}
    

    public void OpenSettingsScene()
    {
        StartCoroutine(EsperarYCargarEscena("MenuSettings")); // Asegúrate que el nombre coincida exactamente con el nombre de tu escena de ajustes
    }
    public void OpenMenu()
    {
        StartCoroutine(EsperarYCargarEscena("MenuPrincipal")); // Asegúrate que el nombre coincida exactamente con el nombre de tu escena de ajustes
    }

    public void QuitGame()
    {
        StartCoroutine(EsperarYSalir());
    }

    IEnumerator EsperarYCargarEscena(string nombreEscena)
    {
        yield return new WaitForSeconds(0.9f); // Retraso para permitir que suene el clic
        SceneManager.LoadScene(nombreEscena);
    }

    IEnumerator EsperarYSalir()
    {
        yield return new WaitForSeconds(0.9f);
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
