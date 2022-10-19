using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InteractableBattery : MonoBehaviour
{
    [Title("States")]
    public ButtonState state = ButtonState.Off;    
    bool active = true;

    [Title("Stats")]
    public float batteryCharge = 10f;


    [FoldoutGroup("Events")]
    [HideLabel]
    public FrameCoreEvent outOfBatteryEvent = new FrameCoreEvent
    {
        eventName = "Out Of Battery"
    };



    // Start is called before the first frame update
    void Start()
    {

    }

    public void TurnOff()
    {
        state = ButtonState.Off;
    }

    public void TurnOn()
    {
        state = ButtonState.On;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (state == ButtonState.On)
            {
                if (batteryCharge <= 0f)
                {
                    outOfBatteryEvent.Activate();
                    active = false;
                };
                batteryCharge -= Time.deltaTime;
            };
        };
    }





}
