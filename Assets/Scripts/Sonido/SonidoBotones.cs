using UnityEngine;
using UnityEngine.EventSystems;

public class BotonSonido : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioClip sonidoEnter;
    public AudioClip sonidoExit;
    public AudioClip sonidoClick;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (sonidoEnter != null)
            audioSource.PlayOneShot(sonidoEnter);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (sonidoExit != null)
            audioSource.PlayOneShot(sonidoExit);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sonidoClick != null)
            audioSource.PlayOneShot(sonidoClick);
    }
}
