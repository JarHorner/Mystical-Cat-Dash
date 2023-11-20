using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private Vector3 centerPoint = new Vector3(0, 0, 0);
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioMixerGroup backgroundAudioMixer;
    [SerializeField] private AudioMixerGroup soundEffectAudioMixer;
    private GameObject bgPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //starts by playing background music when entering first scene
        PlayBackground(backgroundMusic);
    }

    //creates a new GameObject, gives it a AudioSource component and adds the parameter as the clip then plays it.
    //Used for sound effects
    public void Play(AudioClip clip, float volume)
    {
        GameObject newSound = new GameObject();
        newSound.transform.position = this.transform.position;
        newSound.name = clip.name;
        //newSound.transform.parent = gameObject.transform;

        newSound.AddComponent(typeof(AudioSource));
        AudioSource source = newSound.GetComponent<AudioSource>();
        source.outputAudioMixerGroup = soundEffectAudioMixer;
        source.clip = clip;
        source.priority = 0;
        source.volume = volume;
        source.Play();
        Destroy(newSound, clip.length);
    }

    //creates a new GameObject, gives it a AudioSource component and adds the parameter as the clip then plays it.
    //Used for background music. difference between this and Play are the volume and priority levels, also looping.
    private void PlayBackground(AudioClip clip)
    {
        bgPlayer = new GameObject();
        bgPlayer.name = "BG Music";
        bgPlayer.transform.position = this.transform.position;
        bgPlayer.transform.parent = gameObject.transform;

        bgPlayer.AddComponent(typeof(AudioSource));
        AudioSource source = bgPlayer.GetComponent<AudioSource>();
        source.outputAudioMixerGroup = backgroundAudioMixer;
        source.clip = clip;
        source.priority = 0;
        source.loop = true;
        source.Play();
    }

        public void StopBackground()
    {
        AudioSource source = bgPlayer.GetComponent<AudioSource>();
        source.Stop();
    }

    public void changeBackground(AudioClip newClip)
    {
        bgPlayer.GetComponent<AudioSource>().clip = newClip;
        bgPlayer.GetComponent<AudioSource>().Play();
    }
}
