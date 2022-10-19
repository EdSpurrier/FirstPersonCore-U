using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableInput : MonoBehaviour
{


    [Title("Settings")]
    public bool toggleOnTriggerState = false;

    [BoxGroup("Interactable Input")]

    [HorizontalGroup("Interactable Input/Row1")]
    [Button("Trigger Input", ButtonSizes.Medium)]
    private void TriggerInteractableInput()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Trigger();
        };
    }

    [HorizontalGroup("Interactable Input/Row1")]
    [Button("UnTrigger Input", ButtonSizes.Medium)]
    private void UnTriggerInteractableInput()
    {
        if (EditorInteractions.InPlayerButton())
        {
            UnTrigger();
        };
    }

    [PropertySpace(15)]


    [FoldoutGroup("Selectable")]
    [HideLabel]
    public Selectable selectable;

    [HideLabel]
    public OnOffToggleEvent triggerEvents = new OnOffToggleEvent
    {
        eventName = "Trigger Events",
        onEvent = new FrameCoreEvent
        {
            eventName = "Trigger"
        },
        offEvent = new FrameCoreEvent
        {
            eventName = "UnTrigger"
        }
    };



    // Start is called before the first frame update
    void Start()
    {
        selectable.Init();
    }



    public void Trigger()
    {
        if (toggleOnTriggerState)
        {
            triggerEvents.ToggleEvent(true);
        }
        else {
            triggerEvents.ToggleEvent(!triggerEvents.toggleState);
        };
    }




    public void UnTrigger()
    {
        if (toggleOnTriggerState)
        {
            triggerEvents.ToggleEvent(false);
        };
    }
}
