using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class Dialogo : MonoBehaviour
{
    [SerializeField] private float typingTime;
    [SerializeField] private int charsToPlaySound;
    [SerializeField] private bool isPlayerTalking;
    [SerializeField] private AudioClip npcVoice;
    [SerializeField] private AudioClip playerVoice;
    [SerializeField] private GameObject DialogoMark;
    [SerializeField] private GameObject DialogoPanel;
    [SerializeField] private TMP_Text DialogoText;
    [SerializeField, TextArea(4, 6)] private string[] dialogoLineas;

    [Header("Video al finalizar el diálogo")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private bool reproducirVideoAlFinal;

    private bool isPlayerInRange;
    private bool didDialogoStart;
    private int lineIndex;
    private AudioSource audioSource;

    private bool videoActivo;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (videoPlayer != null)
        {
            videoPlayer.gameObject.SetActive(false);
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    void Update()
    {
        // Bloquear todo si el video está activo y reproduciéndose
        if (videoPlayer != null && videoPlayer.gameObject.activeSelf && videoPlayer.isPlaying)
            return;

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogoStart)
            {
                StartDialogo();
            }
            else if (DialogoText.text == dialogoLineas[lineIndex])
            {
                NextDialogoLine();
            }
            else
            {
                StopAllCoroutines();
                DialogoText.text = dialogoLineas[lineIndex];
            }
        }
    }

    private void StartDialogo()
    {
        didDialogoStart = true;
        DialogoPanel.SetActive(true);
        DialogoMark.SetActive(false);
        lineIndex = 0;

        if (MusicaManager.instancia != null)
        {
            MusicaManager.instancia.PausarMusica();
        }

        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogoLine()
{
    lineIndex++;
    if (lineIndex < dialogoLineas.Length)
    {
        StartCoroutine(ShowLine());
    }
    else
    {
        didDialogoStart = false;
        DialogoPanel.SetActive(false);
        DialogoMark.SetActive(true);

        if (MusicaManager.instancia != null)
        {
            MusicaManager.instancia.ReanudarMusica();
        }

        if (reproducirVideoAlFinal && videoPlayer != null)
        {
            videoActivo = true;
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.Play();
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        // 🔽 Esta parte es la NUEVA 🔽
        EventoNPCConBotonQuit eventoEspecial = GetComponent<EventoNPCConBotonQuit>();
        if (eventoEspecial != null)
        {
            eventoEspecial.ActivarEvento();
        }
    }
}


    private void OnVideoFinished(VideoPlayer vp)
    {
        videoActivo = false;
        Time.timeScale = 1f;
        vp.gameObject.SetActive(false);
    }

    private void SelectAudioClip()
    {
        if (lineIndex != 0)
        {
            isPlayerTalking = !isPlayerTalking;
        }

        audioSource.clip = isPlayerTalking ? playerVoice : npcVoice;
    }

    private IEnumerator ShowLine()
    {
        SelectAudioClip();
        DialogoText.text = string.Empty;
        int charIndex = 0;

        foreach (char ch in dialogoLineas[lineIndex])
        {
            DialogoText.text += ch;

            if (charIndex % charsToPlaySound == 0)
            {
                audioSource.Play();
            }

            charIndex++;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            DialogoMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            DialogoMark.SetActive(false);
        }
    }
}
