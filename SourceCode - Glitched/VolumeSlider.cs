using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public GameControl gC;

    public Slider slider;

    public void Start()
    {
        gC = GameControl.GetInstance();
        slider.onValueChanged.AddListener(val => gC.ChangeMasterVolume(val));
    }
}
