using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClipStateEvents : MonoBehaviour
{
    public enum ClipState
    {
        Full,
        Used,
        Empty
    };

    [Title("Text Output")]
    public string preCountText;
    public string postCountText;
    public string emptyText;


    [Title("Settings")]
    public ClipState clipState;

    [Title("Eject Events")]
    public FrameCoreEvent fullEvent;
    public FrameCoreEvent usedEvent;
    public FrameCoreEvent emptyEvent;

    [Title("Parts")]
    public AmmoClip ammoClip;
    public TextMesh textUI;


    

    private void Start()
    {
        UpdateState(true);
    }

    public void UpdateState(bool force = false)
    {

        ClipState oldClipState = clipState;

        UpdateAmmoCount();

        if (oldClipState != clipState || force)
        {
            if (clipState == ClipState.Full)
            {
                fullEvent.Activate();
            }
            else if (clipState == ClipState.Used)
            {
                usedEvent.Activate();
            }
            else if (clipState == ClipState.Empty)
            {
                emptyEvent.Activate();
            };
        };
        
    }


    public void UpdateAmmoCount()
    {
        string outputText = "";

        

        if (ammoClip.currentBullets > 0)
        {
            outputText = preCountText + ammoClip.currentBullets.ToString() + postCountText;

            if (ammoClip.currentBullets == ammoClip.maxBullets)
            {
                clipState = ClipState.Full;
            }
            else {
                clipState = ClipState.Used;
            };

        } else if (ammoClip.currentBullets == 0)
        {
            outputText = emptyText;


            clipState = ClipState.Empty;
        };

        if (textUI)
        {
            textUI.text = outputText;
        };

    }


}
