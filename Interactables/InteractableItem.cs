using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InteractableItem : MonoBehaviour
{
    [Title("Interactable Item")]
    public InteractableItemType itemType;

    


    [FoldoutGroup("Trigger")]
    [Title("Input")]
    public float triggerValue = 0f;

    [Title("Rules")]
    public bool mustBeHeldToTrigger = true;
    [FoldoutGroup("Trigger")]
    public bool unTriggerOnDrop = true;


    [FoldoutGroup("Trigger")]
    [FoldoutGroup("Trigger/Events")]
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

    [FoldoutGroup("Secondary Trigger")]
    [Title("Input")]
    public float triggerSecondaryValue = 0f;

    [Title("Rules")]
    public bool mustBeHeldToSecondaryTrigger = true;
    [FoldoutGroup("Secondary Trigger")]
    public bool unSecondaryTriggerOnDrop = true;


    [FoldoutGroup("Secondary Trigger")]
    [FoldoutGroup("Secondary Trigger/Events")]
    [HideLabel]
    public OnOffToggleEvent secondaryTriggerEvents = new OnOffToggleEvent
    {
        eventName = "Secondary Trigger Events",
        onEvent = new FrameCoreEvent
        {
            eventName = "Trigger Secondary"
        },
        offEvent = new FrameCoreEvent
        {
            eventName = "UnTrigger Secondary"
        }
    };


    [FoldoutGroup("Pick Up")]
    [Title("Pick Up & Drop Events")]
    [HideLabel]
    public OnOffToggleEvent pickupDropEvents = new OnOffToggleEvent
    {
        eventName = "Pick Up & Drop Events",
        onEvent = new FrameCoreEvent
        {
            eventName = "Pickup"
        },
        offEvent = new FrameCoreEvent
        {
            eventName = "Drop"
        }
    };



    [Title("System")]
    public bool inHand = false;


    [Space(15)]
    [HideLabel]
    public DeBugger debug;



    public void Trigger(float triggerValue)
    {
        this.triggerValue = triggerValue;

        if (mustBeHeldToTrigger && !inHand)
        {
            return;
        };

        triggerEvents.ToggleEvent(true);

        debug.Log("InteractableItem >> Trigger()");
    }


    public void UnTrigger()
    {
        triggerValue = 0f;

        triggerEvents.ToggleEvent(false);
    }





    //  SECONDARY TRIGGER
    public void TriggerSecondary(float triggerValue)
    {
        triggerSecondaryValue = triggerValue;

        if (mustBeHeldToSecondaryTrigger && !inHand)
        {
            return;
        };

        secondaryTriggerEvents.ToggleEvent(true);

        debug.Log("InteractableItem >> TriggerSeconday()");
    }


    public void UnTriggerSecondary()
    {
        triggerSecondaryValue = 0f;

        secondaryTriggerEvents.ToggleEvent(false);
    }






    public void Pickup()
    {
        inHand = true;

        pickupDropEvents.ToggleEvent(true);
    }


    public void Drop()
    {
        inHand = false;

        if (unTriggerOnDrop)
        {
            UnTrigger();
        };

        if (unSecondaryTriggerOnDrop)
        {
            UnTriggerSecondary();
        };


        pickupDropEvents.ToggleEvent(false);
    }


}
