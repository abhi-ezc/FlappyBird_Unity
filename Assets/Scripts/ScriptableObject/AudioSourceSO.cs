using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSourceSO", menuName = "Scriptable Objects/AudioSourceSO")]
public class AudioSourceSO : ScriptableObject
{
    public List<AudioClipProperty> AudioClipProperties = new List<AudioClipProperty>();
}

[Serializable]
public class AudioClipProperty
{
    public AudioClip AudioClip;
    public EAudioClipType AudioType;
}
