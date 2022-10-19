using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machin3;

[ExecuteInEditMode]
public class Grenade4Control : MonoBehaviour {

        [Range(0,1)]
        public float slider;

        M3Object hemitop = new M3Object(); 
        M3Object hemibottom = new M3Object(); 

        M3Object insetftop = new M3Object(); 
        M3Object insetbtop = new M3Object(); 
        
        M3Object insetfbottom = new M3Object(); 
        M3Object insetbbottom = new M3Object(); 
        

        void Awake () {

                hemitop.Init   ( new Vector3(-90, 0, 0), null, gameObject.transform.GetChild( 0 ).gameObject);
                hemibottom.Init( new Vector3( 90, 0, 0), null, gameObject.transform.GetChild ( 1).gameObject);

                insetftop.Init( new Vector3( 0, 0, 0), null, hemitop.go.transform.GetChild( 0).gameObject);
                insetbtop.Init( new Vector3( 0, 0, 0), null, hemitop.go.transform.GetChild( 1).gameObject);
                insetfbottom.Init( new Vector3( 0, 0, 0), null, hemibottom.go.transform.GetChild( 0).gameObject);
                insetbbottom.Init( new Vector3( 0, 0, 0), null, hemibottom.go.transform.GetChild( 1).gameObject);
                
        }

	void Update () {

                hemitop.InitTransform();
                hemibottom.InitTransform();
                
                insetftop.InitTransform();
                insetbtop.InitTransform();
                insetfbottom.InitTransform();
                insetbbottom.InitTransform();

                hemitop.Turn   (-45, "Z", slider, 0, 0.5f);
                hemibottom.Turn(-45, "Z", slider, 0, 0.5f);

                insetftop.Turn( 20, "X", slider, 0.7f, 1);
                insetbtop.Turn(-20, "X", slider, 0.7f, 1);
                
                insetfbottom.Turn(-20, "X", slider, 0.7f, 1);
                insetbbottom.Turn( 20, "X", slider, 0.7f, 1);
		
	}
}
