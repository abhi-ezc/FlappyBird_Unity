using UnityEngine;

/// <summary>
/// Manages audio playback for sound effects in the game.
/// Implements the Singleton pattern.
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the AudioManager.
    /// </summary>
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSourceSO audioSourceSO;

    /// <summary>
    /// Ensures only one instance of AudioManager exists.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Plays a sound effect based on the provided audio clip type.
    /// </summary>
    /// <param name="clipType">The type of audio clip to play.</param>
    public void playSoundEffect(EAudioClipType clipType)
    {
        var audioClipProperty = audioSourceSO.AudioClipProperties.Find(item => item.AudioType == clipType);
        if (audioClipProperty != null)
        {
            audioSource.PlayOneShot(audioClipProperty.AudioClip);
        }
    }
}
