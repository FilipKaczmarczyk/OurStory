using System;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Audio/Audio Cue")]
public class AudioCueSO : ScriptableObject
{
    [SerializeField] private AudioClip audioClip;

    public AudioClip AudioClip { get { return audioClip; } }

    public void PlaySFX(Vector3 position)
    {
        SoundManager.PlaySound(this, position);
    }
}

