using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{

    public WeaponController weaponController;
    public Transform ammoAttachmentPoint;

    [FoldoutGroup("Reloading Event")]
    [HideLabel]
    public FrameCoreEvent reloadStartedEvent = new FrameCoreEvent
    {
        eventName = "Reloading"
    };


    [FoldoutGroup("Reload Completed Event")]
    [HideLabel]
    public FrameCoreEvent reloadCompletedEvent = new FrameCoreEvent
    {
        eventName = "Reload Completed"
    };





    public void Trigger()
    {
        if (FPC.core.player.weaponReloader.reloading)
        {
            return;
        };
        weaponController.Trigger();
    }
    public void UnTrigger()
    {
        weaponController.UnTrigger();
    }



    public void ReloadWeapon()
    {
        //  UNTRIGGER WEAPON IF BEING HELD DOWN WHILE RELOADING
        UnTrigger();

        if (FPC.core.player.weaponReloader.ReloadWeapon(this))
        {
            reloadStartedEvent.Activate();
        };
    }


    public void ReloadCompleted()
    {
        reloadCompletedEvent.Activate();
    }


    public void DetachClipFromWeapon()
    {
        weaponController.ammoController.RemoveClip();
    }


    public void AttachClipToHand()
    {

    }


    public void AttachClipToWeapon(AmmoClip ammoClip)
    {
        ammoClip.transform.ParentTo(ammoAttachmentPoint);
        weaponController.ammoController.AddClip(ammoClip);
    }

}
