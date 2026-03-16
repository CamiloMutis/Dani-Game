using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaManager : MonoBehaviour
{
    public static MusicaManager instancia;

    private AudioSource audioSource;

    [SerializeField] private string escenaMusical = "JuegoPrincipal"; // Cambia esto al nombre de tu escena con música

    void Awake()
{
    if (instancia == null)
    {
        instancia = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>(); // <-- mover aquí
        SceneManager.sceneLoaded += VerificarEscena;
    }
    else
    {
        Destroy(gameObject);
    }
}


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        VerificarEscena(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void VerificarEscena(Scene escena, LoadSceneMode modo)
    {
        if (escena.name == escenaMusical)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log("🎵 Música reanudada en " + escena.name);
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                Debug.Log("🔇 Música detenida en " + escena.name);
            }
        }
    }

    public void PausarMusica()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void ReanudarMusica()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
