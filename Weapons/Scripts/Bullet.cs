using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum BulletType
{
    Pistol,
    Shotgun,
    AK,
    Generic,
    eHG,
    rx87
}


public class Bullet : MonoBehaviour
{
    public BulletType bulletType;
    [Title("Bullet Type")]
    [InlineButton("AddToPoolBullet")]
    public Transform bullet;
    [InlineButton("AddToPoolMuzzle")]
    public Transform muzzleFlash;
    [InlineButton("AddToPoolCasing")]
    public Transform casing;


    private void AddToPoolBullet()
    {
        EditorInteractions.AddToPool(bullet);
    }

    private void AddToPoolMuzzle()
    {
        EditorInteractions.AddToPool(muzzleFlash);
    }

    private void AddToPoolCasing()
    {
        EditorInteractions.AddToPool(casing);
    }
}
