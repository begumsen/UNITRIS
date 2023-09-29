using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorLerp : MonoBehaviour
{
    private Image image;
    [SerializeField] private Color[] colors;
    int index = 0;
    float changer;
    float lerpTime = 0.8f;
    void Start()
    {
        image = GetComponent<Image>();
        colors = new Color[6];
        ColorUtility.TryParseHtmlString("#27ae60", out colors[0]);
        ColorUtility.TryParseHtmlString("#d35400", out colors[1]);
        ColorUtility.TryParseHtmlString("#c0392b", out colors[2]);
        ColorUtility.TryParseHtmlString("#8e44ad", out colors[3]);
        ColorUtility.TryParseHtmlString("#2980b9", out colors[4]);
        ColorUtility.TryParseHtmlString("#ECC646", out colors[5]);
        
    }
    void Update()
    {
        image.color = Color.Lerp(image.color, colors[index], lerpTime * Time.deltaTime);

        changer = Mathf.Lerp(changer, 1, lerpTime * Time.deltaTime);

        if (changer > 0.9f)
        {
            changer = 0;
            index++;

            if (index >= colors.Length)
            {
                index = 0;
            }
        }
    }
}