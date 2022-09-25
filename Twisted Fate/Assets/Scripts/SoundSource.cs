using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource source;
    [SerializeField] bool ostMusic = false;

    float startVolume;
    // Start is called before the first frame update
    void Start()
    {
        startVolume = source.volume;
        GameEvents.muteEvent.AddListener(MuteSound);
        GameEvents.unmuteEvent.AddListener(UnmuteSound);
        GameEvents.changeVolumeEvent.AddListener(() => 
            source.volume = startVolume * SoundManager.volumeIntensity
        );
        source.volume = source.volume * SoundManager.volumeIntensity;
        if (ostMusic && !SoundManager.muted) {
            source.Play();
        }
    }

    public void PlaySound() {
        if(!SoundManager.muted)
            source.Play();
    }

    public void StopSound() {
        source.Stop();
    }
    public void MuteSound()
    {
        source.mute = true;
    }
    public void UnmuteSound()
    {
        source.mute = false;
    }
}
