using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioDatabases audioDatabases = null;
    string AudioEventName = "AudioEvent";
    AudioSource audioSource;
    List<AudioSource> audioSources = new List<AudioSource>();
    int playAudioSourcesIndex = 0;
    public bool TestPlayRandomKeySound = false;
    public bool TestPost;

    public override void Awake()
    {
        base.Awake();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSources = new List<AudioSource>(gameObject.GetComponentsInChildren<AudioSource>());
        audioDatabases = Resources.Load<AudioDatabases>("AudioDatabases");
        RegistAudioEvent();
    }

    AudioObserver audioObserver;

    void RegistAudioEvent()
    {
        if (audioObserver == null)
        {
            audioObserver = new AudioObserver();
            NotificationCenter.Default.AddObserver(audioObserver, AudioEventName);
        }
    }

    public class AudioObserver : INotification
    {
        void INotification.OnNotify(Notification _noti)
        {
            Debug.Log("AudioManager.cs Get audio msg: name[" + _noti.name + "], " + "data[" + _noti.data.ToString() + "], ");

            string name = _noti.name;
            string data = (string)_noti.data;

            AudioManager.instance.PlayAudio(data);
        }
    }

    AudioSource GetAudioSources()
    {
        if(audioSources.Count == 0)
        {
            return audioSource;
        }
        AudioSource palyAudioSource = null;

        if(Mathf.Max(audioSources.Count-1, 0) == playAudioSourcesIndex)
        {
            playAudioSourcesIndex = 0;
        }
        else
        {
            playAudioSourcesIndex++;
        }

        palyAudioSource = audioSources[playAudioSourcesIndex];

        return palyAudioSource;
    }

    public void PlayAudio(string autioEventKey)
    {
        if(audioSource == null)
        {
            return;
        }
        AudioClip audioClip = FindAudioClip(autioEventKey);
        
        if(audioClip == null)
        {
            return;
        }

        AudioSource playAudioSource = GetAudioSources();
        playAudioSource.clip = audioClip;
        playAudioSource.Play();
    }

    AudioClip FindAudioClip(string audioEventKey)
    {
        AudioData audioDtata = audioDatabases.datas.Find(e => e.key == audioEventKey);
        if(audioDtata == null)
        {
            return null;
        }
        return audioDtata.audioClip;
    }

    private void Update()
    {
        if (TestPlayRandomKeySound)
        {
            TestPlayRandomKeySound = false;
            string key = audioDatabases.datas[Random.Range(0, audioDatabases.datas.Count)].key;
            PlayAudio(key);
        }
        if (TestPost)
        {
            TestPost = false;
            AudioData audioData = audioDatabases.datas[Random.Range(0, audioDatabases.datas.Count)];
            NotificationCenter.Default.Post(gameObject, AudioEventName, audioData.key);
        }
    }
}
