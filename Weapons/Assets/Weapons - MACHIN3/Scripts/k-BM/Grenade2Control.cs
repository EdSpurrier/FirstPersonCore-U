using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machin3;

[ExecuteInEditMode]
public class Grenade2Control : MonoBehaviour {

        [Range(0,1)]
        public float slider;

        M3Object spoonl = new M3Object(); 
        M3Object spoonr = new M3Object(); 
        M3Object hingeltop = new M3Object(); 
        M3Object hingelbottom = new M3Object(); 
        M3Object hingertop = new M3Object(); 
        M3Object hingerbottom = new M3Object(); 

        M3Object coretop= new M3Object(); 
        M3Object corebottom= new M3Object(); 
        
	void Awake () {

                spoonl.Init( null, new Vector3( 0, 0.07224f, 0), gameObject.transform.GetChild( 0).gameObject);
                spoonr.Init( null, new Vector3( 0, 0.07224f, 0), gameObject.transform.GetChild( 3).gameObject);

                hingeltop.Init   ( new Vector3(-90, 0, 0), null, gameObject.transform.GetChild( 1).gameObject);
                hingelbottom.Init( new Vector3(-90, 0, 0), null, gameObject.transform.GetChild( 2).gameObject);
                
                hingertop.Init   ( new Vector3(-90, 0, 0), null, gameObject.transform.GetChild( 4).gameObject);
                hingerbottom.Init( new Vector3(-90, 0, 0), null, gameObject.transform.GetChild( 5).gameObject);

                coretop.Init   ( new Vector3(-90, 0, 0), new Vector3( 0, 0.142f, 0), gameObject.transform.GetChild( 6).gameObject);
                corebottom.Init( new Vector3(-90, 0, 0), new Vector3( 0, 0.142f, 0), gameObject.transform.GetChild( 7).gameObject);

	}
	
	void Update () {

                spoonl.InitTransform();
                spoonr.InitTransform();
                hingeltop.InitTransform();
                hingelbottom.InitTransform();
                hingertop.InitTransform();
                hingerbottom.InitTransform();
                coretop.InitTransform();
                corebottom.InitTransform();

                spoonl.Move      (-0.008f, "X", slider, 0, 0.3f);
                spoonr.Move      ( 0.008f, "X", slider, 0, 0.3f);
                hingeltop.Turn   (    22, "Y", slider, 0, 0.3f);
                hingelbottom.Turn(   -22, "Y", slider, 0, 0.3f);
                hingertop.Turn   (   -22, "Y", slider, 0, 0.3f);
                hingerbottom.Turn(    22, "Y", slider, 0, 0.3f);

                coretop.Move   ( 0.008f, "Z", slider, 0.5f, 1);
                coretop.Turn   (   -90, "Z", slider, 0.5f, 1);
                corebottom.Move(-0.008f, "Z", slider, 0.5f, 1);
                corebottom.Turn(    90, "Z", slider, 0.5f, 1);
	}
}
