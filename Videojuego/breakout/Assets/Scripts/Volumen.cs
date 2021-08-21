using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumen", 0.5f);
        AudioListener.volume = slider.value;
    }

    public void ChangeSlider(float valor){
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumen", sliderValue);
        AudioListener.volume = slider.value;
    }

}
