using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Dynamic;

public class AmmoClip : MonoBehaviour
{

    [TitleGroup("Bullets")]
    [HorizontalGroup("Bullets/Row1")]
    [SuffixLabel("Current Bullets", Overlay = true)]
    [HideLabel]
    public int currentBullets = 0;


    [HorizontalGroup("Bullets/Row1")]
    [HideLabel]
    [EnumPaging]
    public BulletType bulletType;
    [HorizontalGroup("Bullets/Row1")]
    [InlineButton("ScanBullets")]
    [SuffixLabel("Max Bullets", Overlay = true)]
    [HideLabel]
    public int maxBullets = 10;

    [TitleGroup("Bullets")]
    public List<Bullet> bullets;

    void ScanBullets()
    {
        Bullet[] bulletsFound = GetComponentsInChildren<Bullet>();

        bullets.Clear();

        foreach (Bullet bulletFound in bulletsFound)
        {
            bullets.Add(bulletFound);
            bulletType = bulletFound.bulletType;
        };

        bool error = false;

        bullets.ForEach(bullet => {
            if (bullet.bulletType != bulletType)
            {
                error = true;
            };
        });

        maxBullets = bullets.Count;

        if (error)
        {
            bullets.Clear();
            Debug.LogError("ERROR >> Bullet Types Are Not Matching In Clip!");
            return;
        };

        bullets.ForEach(bullet => {
            bullet.gameObject.SetActive(false);
        });

        UpdateAmmoClip();
    }


    public AmmoClipStateEvents stateEvents;


    [Title("Parts")]
    public GameObject ammoInClipModel;
    public CollisionSound collisionSound;

    


    [HideLabel]
    public OnOffToggleEvent insertEjectEvents = new OnOffToggleEvent
    {
        eventName = "Insert & Eject Events",
        onEvent = new FrameCoreEvent
        {
            eventName = "Insert"
        },
        offEvent = new FrameCoreEvent
        {
            eventName = "Eject"
        }
    };


    [HideLabel]
    public OnOffToggleEvent addTakeBulletEvents = new OnOffToggleEvent
    {
        eventName = "Add Bullet & Take Bullet Events",
        onEvent = new FrameCoreEvent
        {
            eventName = "Add Bullet"
        },
        offEvent = new FrameCoreEvent
        {
            eventName = "Take Bullet"
        }
    };






    void Start()
    {


        if (!collisionSound)
        {
            collisionSound = GetComponent<CollisionSound>();
        }

        if (maxBullets < bullets.Count)
        {
            maxBullets = bullets.Count;
        };

        UpdateAmmoClip();
    }


    public void UpdateAmmoClip()
    {

        currentBullets = bullets.Count;

        if (ammoInClipModel)
        {
            ammoInClipModel.SetActive((currentBullets > 0));
        };

        if (stateEvents)
        {
            stateEvents.UpdateState();
        };
    }



    public void SetCollisionSoundState(bool state)
    {
        if (collisionSound)
        {
            collisionSound.enabled = state;
        };
    }



    public void InsertClip()
    {
        SetCollisionSoundState(false);

        insertEjectEvents.ToggleEvent(true);
    }



    public void EjectClip()
    {
        SetCollisionSoundState(true);

        insertEjectEvents.ToggleEvent(false);
    }



    public Bullet TakeBulletFromClip()
    {

        if (bullets.Count == 0)
        {
            return null;
        };

        Bullet bullet = bullets[0];

        bullets.Remove(bullet);

        UpdateAmmoClip();


        addTakeBulletEvents.ToggleEvent(false);


        return bullet;
    }




    public bool AddBulletToClip(Bullet bullet)
    {
        if (bullet.bulletType != bulletType || bullets.Count >= maxBullets)
        {
            return false;
        }
        else {

            bullets.Add(bullet);

            addTakeBulletEvents.ToggleEvent(true);

            UpdateAmmoClip();

            return true;

        };
    }

}
