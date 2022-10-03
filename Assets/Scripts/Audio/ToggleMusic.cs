using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMusic : MonoBehaviour
{
    [SerializeField] private bool toggleMusic, toggleEffects;

    public void Toggle()
    {
        if(toggleEffects) SoundManager.Instance.ToggleEffects();
        if(toggleMusic) SoundManager.Instance.ToggleMusic();
    }
}
