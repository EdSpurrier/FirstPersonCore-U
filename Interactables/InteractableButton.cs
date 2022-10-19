using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    public TriggerType triggerType;
    public bool toggleButton = true;

    public bool activated = false;
    public bool buttonActive = true;
   

    [Title("Button On")]
    [HideLabel]
    public FrameCoreEvent buttonOnEvent;

    [Title("Button Off")]
    [HideLabel]
    public FrameCoreEvent buttonOffEvent;


    [Title("Button Alert")]
    [HideLabel]
    public FrameCoreEvent buttonAlertEvent;


    [Title("System")]
    [HideLabel]
    public DeBugger debug;


    public void ButtonOn()
    {
        if (!buttonActive)
        {
            buttonAlertEvent.Activate();
            return;
        };


        if (activated)
        {
            return;
        };



        buttonOnEvent.Activate();


        if (triggerType == TriggerType.Single)
        {
            return;
        };


        activated = true;
        
    }

    public void ButtonOff()
    {
        if (!buttonActive)
        {
            buttonAlertEvent.Activate();
            return;
        };


        buttonOffEvent.Activate();

        activated = false;
    }


}
