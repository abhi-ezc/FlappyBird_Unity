using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject for storing audio clip properties for sound effects.
/// </summary>
[CreateAssetMenu(fileName = "AudioSourceSO", menuName = "Scriptable Objects/AudioSourceSO")]
public class AudioSourceSO : ScriptableObject
{
    /// <summary>
    /// List of audio clip properties (clip and type).
    /// </summary>
    public List<AudioClipProperty> AudioClipProperties = new List<AudioClipProperty>();
}

/// <summary>
/// Represents a single audio clip and its type.
/// </summary>
[Serializable]
public class AudioClipProperty
{
    public AudioClip AudioClip;
    public EAudioClipType AudioType;
}
