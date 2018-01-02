using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MoodMusic
{
    public enum Mood
    {
        None,
        Neutral1,
        Neutral2,
        Emotional1,
        Emotional2,
        Anxiety1,
        Anxiety2,
        Mystery1,
        Mystery2,
        Happy1,
        Happy2,
    }

    public Mood mood;
    public AudioClip clip;
}

[RequireComponent (typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public MoodMusic[] musicLibrary;

    private AudioSource jukebox;

    protected void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.activeSceneChanged += OnSceneChanged;

        jukebox = GetComponent<AudioSource>();
        jukebox.loop = true;
    }

    public AudioClip BGMLookup(MoodMusic.Mood mood)
    {
        foreach (MoodMusic music in musicLibrary)
        {
            if (mood == music.mood)
                return music.clip;
        }

        return null;
    }

    public void PlayBGM(MoodMusic.Mood mood)
    {
        jukebox.clip = BGMLookup(mood);
        jukebox.Play();
    }

    public void OnSceneChanged(Scene s1, Scene s2)
    {
        if (LevelSetting.Instance.levelMood == LevelSetting.lastKnownMood)
            return; // Keep playing the same bgm without interuptions

        StartCoroutine(FakeCrossfadeAudio(jukebox, BGMLookup(LevelSetting.Instance.levelMood), 1));
    }

    protected static IEnumerator FakeCrossfadeAudio(AudioSource audioSource, AudioClip targetClip, float time)
    {
        yield return FadeAudio(audioSource, 0, time / 2);

        audioSource.clip = targetClip;
        audioSource.Play();

        yield return FadeAudio(audioSource, 1, time / 2);
    }

    protected static IEnumerator FadeAudio(AudioSource audioSource, float targetVolume, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (Mathf.Abs(audioSource.volume - targetVolume) > 0)
        {
            audioSource.volume -= (startVolume - targetVolume) * Time.deltaTime / FadeTime;
            Debug.Log(audioSource.volume + "  " + targetVolume);

            yield return null;
        }
    }
}

