using Audio.ScriptableObjects;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{
    public static void PlaySound(AudioCueSO audioCueSO, Vector3 position) 
    {
        var soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;
        var audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(audioCueSO.AudioClip);

        Destroy(soundGameObject, audioCueSO.AudioClip.length);
    }
}
