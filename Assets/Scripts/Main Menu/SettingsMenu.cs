using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] Toggle muteToggle;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider bgmVolumeSlider;
    [SerializeField] Slider effectVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        AdjustMenu();
    }

    // Properly the menu with prefab values
    private void AdjustMenu()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVol");
        audioMixer.SetFloat("MasterVolume", masterVolume);
        masterVolumeSlider.value = masterVolume;

        float backgroundVolume = PlayerPrefs.GetFloat("BGVol");
        audioMixer.SetFloat("BGVolume", backgroundVolume);
        bgmVolumeSlider.value = backgroundVolume;

        float effectVolume = PlayerPrefs.GetFloat("SFXVol");
        audioMixer.SetFloat("SFXVolume", effectVolume);
        effectVolumeSlider.value = effectVolume;

        SetMute(PlayerPrefs.GetInt("Mute", 0) != 0);
        muteToggle.isOn = PlayerPrefs.GetInt("Mute", 0) != 0;
    }

    //mutes the master volume, but also disabes the volume slider and changes the navigation so the slider cannot be used.
    public void SetMute(bool isMuted)
    {
        if (isMuted)
        {
            float muteVolume = -80;

            PlayerPrefs.SetInt("Mute", 1);

            masterVolumeSlider.interactable = false;
            bgmVolumeSlider.interactable = false;
            effectVolumeSlider.interactable = false;

            audioMixer.SetFloat("MasterVolume", muteVolume);
        }
        else
        {

            PlayerPrefs.SetInt("Mute", 0);

            masterVolumeSlider.interactable = true;
            bgmVolumeSlider.interactable = true;
            effectVolumeSlider.interactable = true;

            audioMixer.SetFloat("MasterVolume", masterVolumeSlider.value);
        }
    }

    //sets the master volume of the game. (ensure all different groups are affected)
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVol", volume);
    }

    //sets the background volume of the game. (ensure all different groups are affected)
    public void SetBackgroundVolume(float volume)
    {
        audioMixer.SetFloat("BGVolume", volume);
        PlayerPrefs.SetFloat("BGVol", volume);
    }

    //sets the sound effect volume of the game. (ensure all different groups are affected)
    public void SetSoundEffectVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVol", volume);

    }
}
