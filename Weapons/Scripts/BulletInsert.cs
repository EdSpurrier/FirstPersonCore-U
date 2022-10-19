using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInsert : MonoBehaviour {

    public AmmoClip ammoClip;

    public FrameCoreEvent insertEvent = new FrameCoreEvent
    {
        eventName = "Insert Bullet"
    };

    public bool setBulletInactive = true;
    public Transform bulletPosition;



    private void OnTriggerEnter(Collider other) {

        Bullet bullet = other.GetComponent<Bullet>();

        if (bullet != null) {


            // Weapon is full
            if ( !ammoClip.AddBulletToClip(bullet) ) {
                return;
            };


            // Drop the bullet and add ammo to gun
            Debug.LogError("Drop the bullet and add to ammo...");
            /*if (!bullet.grabbable)
            {
                bullet.grabbable.DropItem(true, false);
            }*/


            bullet.gameObject.SetActive(!setBulletInactive);

            bullet.transform.parent = bulletPosition;
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localEulerAngles = Vector3.zero;

            insertEvent.Activate();
        }
    }

}



