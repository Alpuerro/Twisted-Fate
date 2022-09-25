using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Sprite[] volumeButtonSprites;
    [Space]
    [SerializeField] Image muteButtonSprite;
    [SerializeField] Slider volumeSlider;
    [Space] 
    [SerializeField] AudioMixerGroup audioMixer;

    public void UpdatePauseMenu() {
        if (volumeSlider == null) volumeSlider = GetComponentInChildren<Slider>();
        if (SoundManager.muted) muteButtonSprite.sprite = volumeButtonSprites[1];
        else muteButtonSprite.sprite = volumeButtonSprites[0];
        volumeSlider.value = SoundManager.volumeIntensity;
    }

    public void MuteVolume() {
        SoundManager.muted = !SoundManager.muted;
        if (SoundManager.muted) GameEvents.muteEvent.Invoke();
        if (!SoundManager.muted) GameEvents.unmuteEvent.Invoke();
        if (SoundManager.muted) muteButtonSprite.sprite = volumeButtonSprites[1];
        else muteButtonSprite.sprite = volumeButtonSprites[0];

        if (SoundManager.muted)
        {
            audioMixer.audioMixer.SetFloat("AlertsVolume", -80f);
            audioMixer.audioMixer.SetFloat("MusicVolume", -80f);
            audioMixer.audioMixer.SetFloat("UIVolume", -80f);
        }
        else {
            audioMixer.audioMixer.SetFloat("AlertsVolume", volumeSlider.value);
            audioMixer.audioMixer.SetFloat("MusicVolume", volumeSlider.value);
            audioMixer.audioMixer.SetFloat("UIVolume", volumeSlider.value);
        }
    }

    public void UpdateVolume(float value)
    {
        SoundManager.volumeIntensity = value;

        if (SoundManager.muted)
        {
            audioMixer.audioMixer.SetFloat("AlertsVolume", -80f);
            audioMixer.audioMixer.SetFloat("MusicVolume", -80f);
            audioMixer.audioMixer.SetFloat("UIVolume", -80f);
        }
        else
        {
            audioMixer.audioMixer.SetFloat("AlertsVolume", value);
            audioMixer.audioMixer.SetFloat("MusicVolume", value);
            audioMixer.audioMixer.SetFloat("UIVolume", value);
        }

        GameEvents.changeVolumeEvent.Invoke();
    }

    public void CloseGame() {
        SceneTransition.instance.FadeInTransition(SceneNamesspace.SceneNames.Menu, SceneNamesspace.SceneNames.Combat);
    }
}
