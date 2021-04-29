using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] string song, nextSong, prevSong;
    void Start()
    {
        if (prevSong != song)
        {
            AudioManager.instance.Play(song);
        }
    }
    private void OnDisable()
    {
        if (nextSong != song)
        {
            AudioManager.instance.Pause(song);
        }
    }

}
