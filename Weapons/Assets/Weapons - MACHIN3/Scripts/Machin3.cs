using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Machin3 {

        public class M3Object {

                public GameObject go;
                public Vector3? rotation;
                public Vector3? position;
                public string name;

                public void Init(Vector3? rotation, Vector3? position, GameObject go) {
                        this.go = go;
                        this.rotation = rotation;
                        this.position = position;
                        this.name = go.name;

                        this.InitTransform();
                }

                public void InitTransform () {
                        if ( this.rotation != null) {
                                this.go.transform.localRotation = Quaternion.Euler(this.rotation.GetValueOrDefault());
                        }
                        if ( this.position != null ) {
                                this.go.transform.localPosition = this.position.GetValueOrDefault();
                        }
                }

                public void Turn (float angle, string axis, float controller, float start, float end, bool ease = false) 
                {
                        controller = controller.Remap(start, end);
                        if ( ease ) {
                                controller = controller.EaseInOut();
                        }

                        if ( axis == "X") {
                                this.go.transform.Rotate(Vector3.right * controller * angle);
                        }
                        else if ( axis == "Y") {
                                this.go.transform.Rotate(Vector3.up * controller * angle);
                        }
                        else if ( axis == "Z") {
                                this.go.transform.Rotate(Vector3.forward * controller * angle);
                        }
                }

                public void Move (float distance, string axis, float controller, float start, float end, bool ease = false)
                {
                        controller = controller.Remap(start, end);
                        if ( ease ) {
                                controller = controller.EaseInOut();
                        }

                        if ( axis == "X") {
                                this.go.transform.Translate(Vector3.right * controller * distance);
                        }
                        else if ( axis == "Y") {
                                this.go.transform.Translate(Vector3.up * controller * distance);
                        }
                        else if ( axis == "Z") {
                                this.go.transform.Translate(Vector3.forward * controller * distance);
                        }
                }
        }
}
