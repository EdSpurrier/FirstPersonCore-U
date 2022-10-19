using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlasher : MonoBehaviour
{
    public List<LightCurveSingleShot> lightCurveShots;
    


    public void Flash()
    {
        lightCurveShots.ForEach(lightCurve => {
            lightCurve.StartLightCurve();
        });


    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
