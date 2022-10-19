using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machin3;


[ExecuteInEditMode]
public class Grenade1Control : MonoBehaviour {

        [Range(0, 1)]
        public float slider;

        M3Object spoon = new M3Object(); 

	void Awake () {
                spoon.Init(new Vector3(-90, 0, 0), null, gameObject.transform.GetChild(1).gameObject);
	}
	
	void Update () {
                spoon.InitTransform();
                spoon.Turn(22, "X", slider, 0, 1);
	}
}
