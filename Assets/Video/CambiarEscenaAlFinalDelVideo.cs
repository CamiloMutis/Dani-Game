using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CambiarEscenaAlFinalDelVideo : MonoBehaviour
{
    [SerializeField] private string nombreDeLaEscenaDestino;
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += AlTerminarVideo;
    }

    void AlTerminarVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene(nombreDeLaEscenaDestino);
        Debug.Log($"Video terminado. Cambiando a escena: {nombreDeLaEscenaDestino}");
    }
}
