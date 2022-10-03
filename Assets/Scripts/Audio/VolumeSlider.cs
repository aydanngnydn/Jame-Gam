using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeMasterVolume(value));
        volumeSlider = GetComponent<Slider>();
    }
}
