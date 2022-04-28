using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup mixerMaster;
    public static AudioManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatialblend;
            s.source.loop = s.loop;
            s.source.rolloffMode = s.rolloffMode;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;
            s.source.outputAudioMixerGroup = mixerMaster;
            s.source.playOnAwake = false;
        }
    }
    
    public void Play (string clipName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == clipName);
        if (s==null)
        {
            Debug.LogWarning("Sound: " + clipName + "not found!");
            return;
        }
        s.source.Play();

    } 
    
    public void Pause (string clipName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == clipName);
        if (s==null)
        {
            Debug.LogWarning("Sound: " + clipName + "not found!");
            return;
        }
        s.source.Pause();

    }

}
