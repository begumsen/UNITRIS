using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerExampleScript : MonoBehaviour
{
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }
    public void ChooseColorButtonClick()
    {
        ColorPicker.Create(image.color, "Choose a color!", SetColor, ColorFinished, true);
    }
    private void SetColor(Color currentColor)
    {
        Debug.Log("set the color");
        image.color = currentColor;
    }

    private void ColorFinished(Color finishedColor)
    {
        Debug.Log("You chose the color " + ColorUtility.ToHtmlStringRGBA(finishedColor));
    }
}
