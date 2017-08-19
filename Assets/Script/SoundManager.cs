using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    const int MAX_AUDIOSOURCE_POOLING =  10;


    [SerializeField]
    GameController gameController;

    [SerializeField]
    AudioClip goodPerformanceSound;

    [SerializeField]
    AudioClip badPerformanceSound;

    [SerializeField]
    AudioClip titleSoundBG;

    [SerializeField]
    AudioClip inGameSoundBG;


    AudioSource _currentMainBG;

    AudioSource[] _goodPerformancePooling;
    AudioSource[] _badPerformancePooling;


    public void Play(string performance)
    {
        if (performance == "Perfect" || performance == "Good") {
            _Play(_goodPerformancePooling);
        } else if (performance == "Bad" || performance == "Miss") {
            _Play(_badPerformancePooling);
        }
    }


    void Awake()
    {
        _currentMainBG = gameObject.AddComponent<AudioSource>() as AudioSource;
        _currentMainBG.loop = true;

        _goodPerformancePooling = new AudioSource[MAX_AUDIOSOURCE_POOLING];
        _badPerformancePooling = new AudioSource[MAX_AUDIOSOURCE_POOLING];

        for (int i = 0; i < MAX_AUDIOSOURCE_POOLING; i++) {
            _goodPerformancePooling[i] = gameObject.AddComponent<AudioSource>() as AudioSource;
            _badPerformancePooling[i] = gameObject.AddComponent<AudioSource>() as AudioSource;
            
            _goodPerformancePooling[i].clip = goodPerformanceSound;
            _badPerformancePooling[i].clip = badPerformanceSound;
        }
    }

    void Start()
    {
        _currentMainBG.clip = titleSoundBG;
        _currentMainBG.Play();
    }

    void _Play(AudioSource[] lookUpSource)
    {
        foreach (AudioSource source in lookUpSource) {
            if (!source.isPlaying) {
                source.Play();
                break;
            }
        }
    }
}
