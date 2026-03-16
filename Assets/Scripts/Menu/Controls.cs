using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public void LoadControlsScene()
    {
        StartCoroutine(EsperarYCargarEscena("Controls"));
    }

    public void LoadGeneralScene()
    {
        StartCoroutine(EsperarYCargarEscena("General"));
    }

    IEnumerator EsperarYCargarEscena(string nombreEscena)
    {
        yield return new WaitForSeconds(0.9f); // Espera para transición
        SceneManager.LoadScene(nombreEscena);
    }
}
