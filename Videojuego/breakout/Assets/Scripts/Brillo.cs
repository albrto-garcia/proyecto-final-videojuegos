using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image BrilloPanel;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);

        BrilloPanel.color = new Color(BrilloPanel.color.r, BrilloPanel.color.g, BrilloPanel.color.b, slider.value);
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo", sliderValue);
        BrilloPanel.color = new Color(BrilloPanel.color.r, BrilloPanel.color.g, BrilloPanel.color.b, slider.value);

    }
}
