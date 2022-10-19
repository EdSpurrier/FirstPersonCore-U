using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Machin3;


[ExecuteInEditMode]
public class Grenade3Control : MonoBehaviour {

        [Range(0,1)]
        public float slider;

        M3Object coretop = new M3Object(); 
        M3Object corebottom = new M3Object(); 
        
        M3Object panelltop = new M3Object(); 
        M3Object panelrtop = new M3Object(); 
        M3Object panellbottom = new M3Object(); 
        M3Object panelrbottom = new M3Object(); 

        M3Object hingeltop = new M3Object(); 
        M3Object hingertop = new M3Object(); 
        M3Object hingelbottom = new M3Object(); 
        M3Object hingerbottom = new M3Object(); 

        void Awake () {

                coretop.Init   ( new Vector3(-90, 0, 0), new Vector3( 0, 0.06417f, 0), gameObject.transform.GetChild( 0).gameObject);
                corebottom.Init( new Vector3(-90, 0, 0), new Vector3( 0, 0.06417f, 0), gameObject.transform.GetChild( 1).gameObject);

                panelltop.Init   ( null, new Vector3( 0, 0.06417f, 0), gameObject.transform.GetChild( 2).gameObject);
                panelrtop.Init   ( null, new Vector3( 0, 0.06417f, 0), gameObject.transform.GetChild( 3).gameObject);
                panellbottom.Init( null, new Vector3( 0, 0.06417f, 0), gameObject.transform.GetChild( 4).gameObject);
                panelrbottom.Init( null, new Vector3( 0, 0.06417f, 0), gameObject.transform.GetChild( 5).gameObject);

                hingeltop.Init   ( new Vector3(-90, 0, 0), null, gameObject.transform.GetChild( 6).gameObject);
                hingertop.Init   ( new Vector3(-90, 0, 0), null, gameObject.transform.GetChild( 7).gameObject);
                hingelbottom.Init( new Vector3(-90, 0, 0), null, gameObject.transform.GetChild( 8).gameObject);
                hingerbottom.Init( new Vector3(-90, 0, 0), null, gameObject.transform.GetChild( 9).gameObject);
                
        }

	void Update () {

                coretop.InitTransform();
                corebottom.InitTransform();

                panelltop.InitTransform();
                panelrtop.InitTransform();
                panellbottom.InitTransform();
                panelrbottom.InitTransform();

                hingeltop.InitTransform();
                hingertop.InitTransform();
                hingelbottom.InitTransform();
                hingerbottom.InitTransform();

                coretop.Move   ( 0.006f, "Z", slider, 0.3f, 0.6f);
                corebottom.Move(-0.006f, "Z", slider, 0.3f, 0.6f);

                coretop.Turn   ( 90, "Z", slider, 0.7f, 1);
                corebottom.Turn(-90, "Z", slider, 0.7f, 1);

                panelltop.Move   (-0.004f, "X", slider,  0.01f, 0.1f  );
                panelrtop.Move   ( 0.004f, "X", slider,      0, 0.09f );
                panellbottom.Move(-0.004f, "X", slider,      0, 0.085f);
                panelrbottom.Move( 0.004f, "X", slider, 0.015f, 0.1f  );

                hingeltop.Turn   (-27, "Y", slider,  0.01f, 0.1f  );
                hingertop.Turn   ( 27, "Y", slider,      0, 0.09f );
                hingelbottom.Turn( 27, "Y", slider,      0, 0.085f);
                hingerbottom.Turn(-27, "Y", slider, 0.015f, 0.1f  );
	}
}
