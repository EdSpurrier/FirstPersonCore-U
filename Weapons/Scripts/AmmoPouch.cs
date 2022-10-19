using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPouch : MonoBehaviour
{
    [System.Serializable]
    public class AttachableAmmo
    {
        public BulletType bulletType;
        public Transform prefab;
        public Transform spawnLocation;
        public int amount = 0;
    }



    [Title("Ammo Types")]
    public List<AttachableAmmo> ammo;

    public AmmoClip GetAmmo(BulletType bulletType)
    {
        
        foreach (AttachableAmmo attachableAmmo in ammo)
        {
            if (attachableAmmo.bulletType == bulletType)
            {
                if (attachableAmmo.amount > 0)
                {
                    attachableAmmo.amount--;

                    GameObject spawn = attachableAmmo.prefab.SpawnObject(Vector3.zero, Quaternion.identity);
                    spawn.transform.ParentTo(attachableAmmo.spawnLocation);
                    AmmoClip ammoClip = spawn.GetComponent<AmmoClip>();
                    return ammoClip;
                }
                else {
                    return null;
                };
            }
        };

        return null;
    }


}
