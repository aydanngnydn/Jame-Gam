using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
        SoundManager.Instance.ChangeMasterVolume(volumeSlider.value);
        volumeSlider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeMasterVolume(value));
    }
}
