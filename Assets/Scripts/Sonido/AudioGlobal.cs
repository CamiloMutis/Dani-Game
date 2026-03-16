using UnityEngine;

public class PersistenteAudio : MonoBehaviour
{
    public static PersistenteAudio instancia;

    private AudioSource audioSource;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Si ya hay uno, este se elimina automáticamente
        }
    }

    public void DetenerMusica()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}
