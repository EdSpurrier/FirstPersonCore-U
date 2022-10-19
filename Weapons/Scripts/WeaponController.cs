using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;


public enum FireType
{
    SingleShot,
    SemiAutomatic,
    FullyAutomatic,
    BurstFire
}


[System.Serializable]
public class BarrelPoint
{
    public Transform barrelPoint;
    public Transform muzzleFlashPoint;
}

public class WeaponController : MonoBehaviour
{

    [Title("Settings")]
    public FireType fireType;
    [ShowIf("@this.fireType == FireType.FullyAutomatic || this.fireType == FireType.BurstFire")]
    public float fireRate = 0.05f;
    [ShowIf("@this.fireType == FireType.BurstFire")]
    public int burstCount = 5;


    [Title("Recoil")]
    public bool recoil = true;
    [ShowIf("recoil")]
    public string recoilComponent;  //  INSERT RECOIL COMPONENT HERE






    [Title("System")]
    public bool fireActive = false;
    public float currentFireRate = 0f;
    public float currentBurstCount = 0f;
    public int currenBarrelPoint = 0;

    [Title("Shell Casing Ejector")]
    public bool shellCasingEjectorActive = true;
    [ShowIf("shellCasingEjectorActive")]
    public Transform shellCasingEjectPoint;
    [ShowIf("shellCasingEjectorActive")]
    public Vector3 pushDirection;
    [ShowIf("shellCasingEjectorActive")]
    [HideLabel]
    public MaxMinFloat ejectVelocity = new MaxMinFloat {
        min = 0.16f,
        max = 0.25f
    };
    [ShowIf("shellCasingEjectorActive")]
    public bool debugEjector = false;
    [ShowIf("@this.shellCasingEjectorActive && this.debugEjector")]
    [InlineButton("TestEjectShell")]
    public Transform casingDebugPrefab;
    public void TestEjectShell()
    {
        if (EditorInteractions.InPlayerButton())
        {
            EjectShell(casingDebugPrefab);
        };
    }


    [Title("Barrel Points")]
    public bool resetBarrelPointOnUnTrigger = false;
    public List<BarrelPoint> barrelPoints;


    [Title("Parts")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    public AmmoController ammoController;


    [Title("Fire Trigger")]
    public FrameCoreEvent fireEvent = new FrameCoreEvent
    {
        eventName = "Fire"
    };

    


    [Title("Empty Fire Trigger")]
    public FrameCoreEvent emptyFireEvent = new FrameCoreEvent
    {
        eventName = "Empty Fire"
    };

    


    [Space(15)]
    [HideLabel]
    public DeBugger debug;
    [FoldoutGroup("Debug Testing")]
    [HorizontalGroup("Debug Testing/Row1", 0.5f)]
    [Button("Trigger Down", ButtonSizes.Large), GUIColor(1f, 0.8f, 1f)]
    [FoldoutGroup("Debug Testing")]
    void TestTriggerDown()
    {
        if (EditorInteractions.InPlayerButton())
        {
            UnTrigger();
        };
    }
    [FoldoutGroup("Debug Testing")]
    [HorizontalGroup("Debug Testing/Row1", 0.5f)]
    [Button("Trigger Up", ButtonSizes.Large), GUIColor(0.8f, 1f, 1f)]
    void TestTriggerUp()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Trigger();
        };
    }



    [PropertySpace(SpaceBefore = 30, SpaceAfter = 30)]
    [Button("Refresh / Setup", ButtonSizes.Medium), GUIColor(0.5f, 0.95f, 0.4f)]
    private void Refresh()
    {
        
        debug.Log("Connect Recoil Component when built here...");
/*
        if (recoil && !recoilComponent)
        {
            recoilComponent = grabbableItemVR.GetComponent<HVRRecoil>();
        };*/

    }

    void Start()
    {
        
        pushDirection.Normalize();

        fireActive = false;
    }


    private void Update()
    {
        if (fireActive)
        {
            if (0f > currentFireRate)
            {
                Fire();
            };

            currentFireRate -= Time.deltaTime;
        };

    }



    public bool WeaponBeingHeld()
    {
        return true;
    }

    public void ResetFire()
    {
        fireActive = false;
        currentBurstCount = 0;
        currentFireRate = 0;

        if (resetBarrelPointOnUnTrigger)
        {
            currenBarrelPoint = 0;
        };

    }

    public void UnTrigger()
    {
        ResetFire();
    }


    public void Trigger()
    {
        currentBurstCount = 0;
        fireActive = true;

        Fire();

        if (fireType == FireType.SingleShot)
        {
            ammoController.Unload();
            ResetFire();
        } else if (fireType == FireType.SemiAutomatic)
        {
            ResetFire();
        };
    }

    
    public void Fire()
    {
        Bullet bullet = ammoController.CheckAmmo();

        if (bullet)
        {
            FireBullet(bullet);
            currentFireRate = fireRate;
            currentBurstCount++;

            if (fireType == FireType.BurstFire)
            {
                if (currentBurstCount > burstCount)
                {
                    ResetFire();
                };
            };
        }
        else
        {
            emptyFireEvent.Activate();

            fireActive = false;
        };
    }


    public void EjectShell(Transform casing)
    {
        if (shellCasingEjectorActive)
        {
            GameObject casingSpawn = casing.SpawnObject(shellCasingEjectPoint.position, shellCasingEjectPoint.rotation);
            Rigidbody rb = casingSpawn.GetComponent<Rigidbody>();

            RandomSeedGenerator.RandomizeSeed();
            rb.AddForce(pushDirection * Random.Range(ejectVelocity.min, ejectVelocity.max), ForceMode.Impulse);

            casingSpawn.transform.parent = null;
            debug.Log("Spawned Casing >> " + casingSpawn.name);
        };
    }

    public BarrelPoint GetBarrelPoint ()
    {
        BarrelPoint barrelPoint = barrelPoints[currenBarrelPoint];


        currenBarrelPoint++;

        if (currenBarrelPoint > (barrelPoints.Count-1))
        {
            currenBarrelPoint = 0;
        };


        return barrelPoint;
    }


    public void FireBullet(Bullet bullet)
    {
        BarrelPoint barrelPoint = GetBarrelPoint();

        GameObject bulletSpawn = bullet.bullet.SpawnObject(barrelPoint.barrelPoint.position, barrelPoint.barrelPoint.rotation);
        bulletSpawn.transform.parent = null;
        debug.Log("Spawned Bullet >> " + bulletSpawn.name);

        GameObject muzzleSpawn = bullet.muzzleFlash.SpawnObject(barrelPoint.muzzleFlashPoint.position, barrelPoint.muzzleFlashPoint.rotation);
        muzzleSpawn.transform.parent = null;       
        debug.Log("Spawned MuzzleFlash >> " + bulletSpawn.name);

        EjectShell(bullet.casing);

        fireEvent.Activate();


        if(recoil)
        {
            debug.Log("[Recoil Triggered]=>Add recoil trigger here....");
            //recoilComponent.Recoil();
        };

        Destroy(bullet.gameObject);
    }


}
