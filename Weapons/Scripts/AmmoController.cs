using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class AmmoController : MonoBehaviour
{
    [Title("Settings")]
    public BulletType bulletType;

    [Title("Loaded System")]
    public bool loaded = true;
    public List<SlideTrigger> slideLoaders;


    [Title("Loaded Clip")]
    public bool permenantClip = false;
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    public AmmoClip loadedAmmoClip;


    [Title("Clip Empty")]
    public FrameCoreEvent clipEmptyEvent = new FrameCoreEvent
    {
        eventName = "Clip Empty"
    };


    [Title("Load Clip")]
    public FrameCoreEvent loadClipEvent = new FrameCoreEvent
    {
        eventName = "Load Clip"
    };

    [Title("Unload Clip")]
    public FrameCoreEvent unloadClipEvent = new FrameCoreEvent
    {
        eventName = "Unload Clip"
    };


    private void Start()
    {
        if (permenantClip)
        {
            loadedAmmoClip.bulletType = bulletType;
        };
    }

    public void Unload()
    {
        foreach (SlideTrigger slideLoader in slideLoaders)
        {
            slideLoader.ResetTrigger();
        };
    }

    public void CheckLoaded()
    {
        IsLoaded();
    }

    public bool IsLoaded()
    {
        foreach(SlideTrigger slideLoader in slideLoaders)
        {
            if (!slideLoader.slideActivated)
            {
                return false;
            };
        };

        return true;
    }



    public Bullet CheckAmmo()
    {
        //  AMMO IN CHAMBER
        if (!loaded) 
        {
            if (!IsLoaded())
            {
                return null;
            };
        };


        if (!loadedAmmoClip)
        {
            return null;
        };


        Bullet bullet = loadedAmmoClip.TakeBulletFromClip();

        if (!bullet)
        {
            return null;
        };

        //  IF CLIP IS EMPTY EVENT
        if (loadedAmmoClip.bullets.Count == 0)
        {
            clipEmptyEvent.Activate();
        };

        return bullet;
    }


    public bool RemoveClip()
    {
        if (!loadedAmmoClip || permenantClip)
        {
            return false;
        }
        else
        {
            loadedAmmoClip.EjectClip();
            loadedAmmoClip = null;

            Debug.Log("Unloaded Clip");
            //  UNLOAD CLIP EVENT
            unloadClipEvent.Activate();

            return true;
        };
    }


    public bool AddClip(AmmoClip clip)
    {

        //Debug.Log("clip.bulletType: " + clip.bulletType  + " bulletType: " + bulletType);
        //Debug.Log("loadedAmmoClip: " + loadedAmmoClip + " permenantClip: " + permenantClip);

        if (clip.bulletType != bulletType || loadedAmmoClip || permenantClip)
        {
            return false;
        }
        else
        {
            loadedAmmoClip = clip;
            loadedAmmoClip.InsertClip();
            Debug.Log("Added Clip");
            //  LOAD CLIP EVENT
            loadClipEvent.Activate();
            
            return true;
        };
    }

}
