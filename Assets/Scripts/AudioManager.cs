using UnityEngine.Audio;
using System;
using UnityEngine;
using RGVA;



public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;


    // Start is called before the first frame update
    private void OnEnable()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;


        }

        Play("BGM");


        
    }
    

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
            return;
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
            return;
        s.source.Stop();
    }
}
