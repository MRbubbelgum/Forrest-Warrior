using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {

        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    [ContextMenu("BorderTransparent")]
    public void borderTransparent()
    {
       
    }

    [ContextMenu("FillTransparent")]
    public void fillTransparent()
    {
        Gradient newGradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = new Color(0f, 0f, 0f, 0f); // Transparent color
        colorKeys[0].time = 0.0f;
        colorKeys[1].color = new Color(0f, 0f, 0f, 0f); // Transparent color
        colorKeys[1].time = 1.0f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 0.0f; // Transparent alpha
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].alpha = 0.0f; // Transparent alpha
        alphaKeys[1].time = 1.0f;

        newGradient.SetKeys(colorKeys, alphaKeys);

        gradient = newGradient;

        // Apply the transparent gradient to the fill color
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
    public void fillColor()
    {
        Gradient newGradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = new Color(255f, 0f, 0f, 255f); // Transparent color
        colorKeys[0].time = 0.0f;
        colorKeys[1].color = new Color(255f, 0f, 0f, 255f); // Transparent color
        colorKeys[1].time = 1.0f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1f; // Transparent alpha
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].alpha = 1f; // Transparent alpha
        alphaKeys[1].time = 1.0f;

        newGradient.SetKeys(colorKeys, alphaKeys);

        gradient = newGradient;

        // Apply the transparent gradient to the fill color
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
