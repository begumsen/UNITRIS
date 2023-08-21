using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleIcon : EventInvoker
{
    public Sprite enabledIcon;
    public Sprite disabledIcon;
    // default state of the icon toggle
    public bool iconState = true;

    // the UI.Image component
    Image m_image;


    // Use this for initialization
    void Start()
    {
        iconState = true;
        m_image = GetComponent<Image>();
        m_image.sprite = (iconState) ? enabledIcon : disabledIcon;
        events.Add(EventName.MusicToggleEvent, new MusicToggleEvent());
        EventManager.AddInvoker(EventName.MusicToggleEvent, this);
        events.Add(EventName.FxToggleEvent, new FxToggleEvent());
        EventManager.AddInvoker(EventName.FxToggleEvent, this);
    }

    public void ToggleIconMusic()
    {
        if (!m_image || !enabledIcon || !disabledIcon)
        {
            Debug.LogWarning("ICONTOGGLE Undefined iconTrue and/or iconFalse!");
            return;
        }
        iconState = !iconState;
        m_image.sprite = (iconState) ? enabledIcon : disabledIcon;
        events[EventName.MusicToggleEvent].Invoke(0);
    }

    public void ToggleIconFx()
    {
        if (!m_image || !enabledIcon || !disabledIcon)
        {
            Debug.LogWarning("ICONTOGGLE Undefined iconTrue and/or iconFalse!");
            return;
        }
        iconState = !iconState;
        m_image.sprite = (iconState) ? enabledIcon : disabledIcon;
        events[EventName.FxToggleEvent].Invoke(0);

    }
}
