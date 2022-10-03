using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource[] musicSources;
    [SerializeField] private AudioSource effectSource;


    private void OnEnable()
    {
        //SceneManager.activeSceneChanged += ChangeActiveMusic;
    }

    private void OnDisable()
    {
        //SceneManager.activeSceneChanged -= ChangeActiveMusic;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        effectSource.mute = !effectSource.mute;
    }

    public void ToggleMusic()
    {
        foreach (AudioSource audioSource in musicSources)
            audioSource.mute = !audioSource.mute;
    }

    private void ChangeActiveMusic(Scene current, Scene next)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicSources[0].mute = false;
            musicSources[1].mute = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("1");
            musicSources[0].mute = true;
            musicSources[1].mute = false;
        }
    }
}
