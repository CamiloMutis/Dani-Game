using UnityEngine;

public class PlayerSoundControler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoSaltar;
    public AudioClip sonidoCorrer;



    public void playSaltar()
    {
        audioSource.PlayOneShot(sonidoSaltar);
    }
    public void playCorrer()
    {
        audioSource.PlayOneShot(sonidoCorrer);
    }   
    }

    
