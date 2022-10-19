using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonPlayerCore : MonoBehaviour
{
    [Title("Player Controller")]
    public FirstPersonPlayerController playerController;

    [Title("Player Inventory")]
    public Inventory inventory;

    [Title("Weapon Reloader")]
    public WeaponReloader weaponReloader;

}
