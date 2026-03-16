using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoSaltar;
    public AudioClip sonidoCorrer;
    public AudioClip sonidoGolpe;
    public AudioClip sonidoAtaque;



    public void playSaltar()
    {
        audioSource.PlayOneShot(sonidoSaltar);
    }
    public void playCorrer()
    {
        audioSource.PlayOneShot(sonidoCorrer);
    }
    public void playGolpe()
    {
        audioSource.PlayOneShot(sonidoGolpe);
    }
    public void playAtaque()
    {
        audioSource.PlayOneShot(sonidoAtaque);
    }
    
}

    
