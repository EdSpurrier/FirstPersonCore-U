using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    public WeaponManager currentWeaponManager;
    public AmmoClip ammoClip = null;

    public bool reloading = false;

    [FoldoutGroup("Reloading Event")]
    [HideLabel]
    public FrameCoreEvent reloadingEvent = new FrameCoreEvent
    {
        eventName = "Reloading"
    };

    [FoldoutGroup("No Ammo Event")]
    [HideLabel]
    public FrameCoreEvent noAmmoEvent = new FrameCoreEvent
    {
        eventName = "No Ammo"
    };

    [FoldoutGroup("Reload Completed Event")]
    [HideLabel]
    public FrameCoreEvent reloadCompletedEvent = new FrameCoreEvent
    {
        eventName = "Reload Completed"
    };




    


    public bool ReloadWeapon(WeaponManager weaponManager)
    {
        currentWeaponManager = weaponManager;


        if (reloading)
        {
            return false;
        };


        ammoClip = FPC.core.player.inventory.ammoPouch.GetAmmo(currentWeaponManager.weaponController.ammoController.bulletType);

        if (!ammoClip)
        {
            noAmmoEvent.Activate();
            return false;
        };


        reloading = true;

        reloadingEvent.Activate();

        return true;
    }

    public void ReloadCompletedCallback()
    {
        reloading = false;
        currentWeaponManager.ReloadCompleted();
        reloadCompletedEvent.Activate();
        ResetReload();
    }

    public void ResetReload()
    {
        reloading = false;
        currentWeaponManager = null;
        ammoClip = null;
    }


    public void RemoveClip()
    {
        //  REMOVE CLIP
        currentWeaponManager.weaponController.ammoController.RemoveClip();
    }


    public void AddClip()
    {
        //  ADD CLIP
        currentWeaponManager.weaponController.ammoController.AddClip(ammoClip);
    }



    public void DetachClipFromWeapon()
    {
        //  DETACH CLIP
        currentWeaponManager.DetachClipFromWeapon();
    }






    public void AttachClipToWeapon()
    {
        //  ATTACH CLIP
        currentWeaponManager.AttachClipToWeapon(ammoClip);
    }

}
