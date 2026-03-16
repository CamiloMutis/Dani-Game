using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    public GameObject restartButton;

    public GameObject salirButton;
    private PlayerControls playerControls;
    private float vidaMaxima;

    void Start()
    {
        GameObject jugador = GameObject.Find("Sonic");
        if (jugador != null)
        {
            playerControls = jugador.GetComponent<PlayerControls>();
            if (playerControls != null)
                vidaMaxima = playerControls.vida;
        }

        if (restartButton != null)
            restartButton.SetActive(false);
        if (salirButton != null)
            salirButton.SetActive(false);
        
    }
    

    void Update()
    {
        if (playerControls != null && rellenoBarraVida != null && vidaMaxima > 0)
        {
            rellenoBarraVida.fillAmount = playerControls.vida / vidaMaxima;

            if (playerControls.vida <= 0 && !restartButton.activeSelf)
            {
                restartButton.SetActive(true);
                salirButton.SetActive(true);
            }
        }
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1; // Reanuda el tiempo antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SalirDelJuego()
{
    Application.Quit();
}

}
