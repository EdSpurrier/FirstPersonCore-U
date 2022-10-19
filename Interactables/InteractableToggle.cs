using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableToggle : MonoBehaviour
{
    [Title("Settings")]
    public bool initialToggleState = false;
    public bool triggerInitalToggleStateOnStart = false;
    

    [HideLabel]
    public OnOffToggleEvent toggleEvents = new OnOffToggleEvent
    {
        eventName = "Toggle Events",
        toggleEvents = true,
        onEvent = new FrameCoreEvent
        {
            eventName = "Toggle On"
        },
        offEvent = new FrameCoreEvent
        {
            eventName = "Toggle Off"
        }
    };



    // Start is called before the first frame update
    void Start()
    {
        if (triggerInitalToggleStateOnStart)
        {
            
            toggleEvents.toggleState = !initialToggleState;
            toggleEvents.ToggleEvent(initialToggleState);
        }
        else {
            toggleEvents.toggleState = initialToggleState;
        };
    }


    public void Toggle()
    {
        toggleEvents.ToggleEvent(!toggleEvents.toggleState);
    }

    public void SetToggleState(bool newState)
    {
        toggleEvents.ToggleEvent(newState);
    }



    public void ToggleOff()
    {
        toggleEvents.ToggleEvent(false);
    }

    public void ToggleOn()
    {
        toggleEvents.ToggleEvent(true);
    }



}
