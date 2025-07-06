using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSourceSO audioSourceSO;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playSoundEffect(EAudioClipType clipType)
    {
        var audioClipProperty = audioSourceSO.AudioClipProperties.Find(item => item.AudioType == clipType);
        if (audioClipProperty != null) {
            audioSource.PlayOneShot(audioClipProperty.AudioClip);
        }
    }
}
