using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    public AmmoController ammoController;
    public int currentAmmoCount = -1;
    public TextMesh textUI;

    public string preCountText;
    public string postCountText;

    public string emptyText;

    public string unloadedText;

    private void Start()
    {
        currentAmmoCount = -666;
    }

    public void Update()
    {
        if (!ammoController.loadedAmmoClip)
        {
            if (currentAmmoCount != -1)
            {
                textUI.text = unloadedText;
                currentAmmoCount = -1;
            };
        }
        else if (currentAmmoCount != ammoController.loadedAmmoClip.currentBullets)
        {
            UpdateAmmoCount();   
        };
    }


    public void UpdateAmmoCount()
    {
        if (ammoController.loadedAmmoClip.currentBullets > 0)
        {
            textUI.text = preCountText + ammoController.loadedAmmoClip.currentBullets.ToString() + postCountText;
        } else if (ammoController.loadedAmmoClip.currentBullets == 0)
        {
            textUI.text = emptyText;
        };

        currentAmmoCount = ammoController.loadedAmmoClip.currentBullets;
    }


}
