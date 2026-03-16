using UnityEngine;

public class DetenerMusicaDesdeBoton : MonoBehaviour
{
    public void DetenerAudio()
    {
        // Detiene la música desde la instancia global PersistenteAudio
        if (PersistenteAudio.instancia != null)
        {
            PersistenteAudio.instancia.DetenerMusica();
        }
    }
}
