/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class AmmoClipInsert : MonoBehaviour {

    [Title("Parts")]
    public Transform clipAttachmentPoint;
    /// <summary>
    /// The weapon this magazine is attached to (optional)
    /// </summary>
    public Grabbable AttachedWeapon;

    [Title("Ammo Controller")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    public AmmoController ammoController;


    [Title("Insert Event")]
    public FrameCoreEvent insertEvent = new FrameCoreEvent
    {
        eventName = "Insert Clip"
    };

    [Title("Eject Event")]
    public FrameCoreEvent ejectEvent = new FrameCoreEvent
    {
        eventName = "Eject Clip"
    };





    public GrabberArea grabClipArea;






    // Lock in place for physics
    bool lockedInPlace = false;

    bool magazineInPlace = false;


    public float MagazineDistance = 0f;
    public float ClipSnapDistance = 0.075f;
    public float ClipUnsnapDistance = 0.15f;



    public Grabbable HeldMagazine = null;
    public Collider HeldCollider = null;


    /// <summary>
    ///  How much force to apply to the inserted magazine if it is forcefully ejected
    /// </summary>
    public float EjectForce = 1f;


    float lastEjectTime;


    void checkGrabClipInput()
    {

        // No need to check for grabbing a clip out if none exists
        if (HeldMagazine == null || grabClipArea == null)
        {
            return;
        }

        // Don't grab clip if the weapon isn't being held
        if (AttachedWeapon != null && !AttachedWeapon.BeingHeld)
        {
            return;
        }

        Grabber nearestGrabber = grabClipArea.GetOpenGrabber();
        if (grabClipArea != null && nearestGrabber != null)
        {
            if (nearestGrabber.HandSide == ControllerHand.Left && InputBridge.Instance.LeftGripDown)
            {
                // grab clip
                OnGrabClipArea(nearestGrabber);
            }
            else if (nearestGrabber.HandSide == ControllerHand.Right && InputBridge.Instance.RightGripDown)
            {
                OnGrabClipArea(nearestGrabber);
            }
        }
    }



    // Pull out magazine from clip area
    public void OnGrabClipArea(Grabber grabbedBy)
    {
        if (HeldMagazine != null)
        {
            // Store reference so we can eject the clip first
            Grabbable temp = HeldMagazine;

            // Make sure the magazine can be gripped
            HeldMagazine.enabled = true;

            // Eject clip into hand
            detachMagazine();

            // Now transfer grab to the grabber
            temp.enabled = true;

            grabbedBy.GrabGrabbable(temp);
        }
    }

    void LateUpdate()
    {
        // Are we trying to grab the clip from the weapon
        checkGrabClipInput();


        // There is a magazine inside the slide. Position it properly
        if (HeldMagazine != null)
        {

            HeldMagazine.transform.parent = transform;

            // Lock in place immediately
            if (lockedInPlace)
            {
                HeldMagazine.transform.localPosition = Vector3.zero;
                HeldMagazine.transform.localEulerAngles = Vector3.zero;
                return;
            }

            Vector3 localPos = HeldMagazine.transform.localPosition;

            // Make sure magazine is aligned with MagazineSlide
            HeldMagazine.transform.localEulerAngles = Vector3.zero;

            // Only allow Y translation. Don't allow to go up and through clip area
            float localY = localPos.y;
            if (localY > 0)
            {
                localY = 0;
            }

            moveMagazine(new Vector3(0, localY, 0));

            MagazineDistance = Vector3.Distance(transform.position, HeldMagazine.transform.position);

            bool clipRecentlyGrabbed = Time.time - HeldMagazine.LastGrabTime < 1f;

            // Snap Magazine In Place
            if (MagazineDistance < ClipSnapDistance)
            {

                // Snap in place
                if (!magazineInPlace && !recentlyEjected() && !clipRecentlyGrabbed)
                {
                    attachMagazine();
                }

                // Make sure magazine stays in place if not being grabbed
                if (!HeldMagazine.BeingHeld)
                {
                    moveMagazine(Vector3.zero);
                }
            }
            // Stop aligning clip with slide if we exceed this distance
            else if (MagazineDistance >= ClipUnsnapDistance && !recentlyEjected())
            {
                detachMagazine();
            }
        }
    }



    bool recentlyEjected()
    {
        return Time.time - lastEjectTime < 0.1f;
    }


    void moveMagazine(Vector3 localPosition)
    {
        HeldMagazine.transform.localPosition = localPosition;
    }



    void attachMagazine()
    {
        // Drop Item
        var grabber = HeldMagazine.GetPrimaryGrabber();
        HeldMagazine.DropItem(grabber, false, false);


        // Move to desired location before locking in place
        moveMagazine(Vector3.zero);

        // Add fixed joint to make sure physics work properly
        if (transform.parent != null)
        {
            Rigidbody parentRB = transform.parent.GetComponent<Rigidbody>();
            if (parentRB)
            {
                FixedJoint fj = HeldMagazine.gameObject.AddComponent<FixedJoint>();
                fj.autoConfigureConnectedAnchor = true;
                fj.axis = new Vector3(0, 1, 0);
                fj.connectedBody = parentRB;
            }


        }

        // Don't let anything try to grab the magazine while it's within the weapon
        // We will use a grabbable proxy to grab the clip back out instead
        HeldMagazine.enabled = false;

        lockedInPlace = true;
        magazineInPlace = true;
    }




    /// <summary>
    /// Detach Magazine from it's parent. Removes joint, re-enables collider, and calls events
    /// </summary>
    /// <returns>Returns the magazine that was ejected or null if no magazine was attached</returns>
    Grabbable detachMagazine()
    {

        if (HeldMagazine == null)
        {
            return null;
        }

        EjectClip();
        


        HeldMagazine.transform.parent = null;

        // Remove fixed joint
        if (transform.parent != null)
        {
            Rigidbody parentRB = transform.parent.GetComponent<Rigidbody>();
            if (parentRB)
            {
                FixedJoint fj = HeldMagazine.gameObject.GetComponent<FixedJoint>();
                if (fj)
                {
                    fj.connectedBody = null;
                    Destroy(fj);
                }
            }
        }

        // Reset Collider
        if (HeldCollider != null)
        {
            HeldCollider.enabled = true;
            HeldCollider = null;
        }


        // Can be grabbed again
        HeldMagazine.enabled = true;
        magazineInPlace = false;
        lockedInPlace = false;
        lastEjectTime = Time.time;

        var returnGrab = HeldMagazine;
        HeldMagazine = null;

        

        return returnGrab;
    }


    public void EjectMagazine()
    {
        Grabbable ejectedMag = detachMagazine();
        lastEjectTime = Time.time;

        StartCoroutine(EjectMagRoutine(ejectedMag));
    }



    IEnumerator EjectMagRoutine(Grabbable ejectedMag)
    {

        if (ejectedMag != null && ejectedMag.GetComponent<Rigidbody>() != null)
        {
            EjectClip();

            Rigidbody ejectRigid = ejectedMag.GetComponent<Rigidbody>();

            // Wait before ejecting

            // Move clip down before we eject it
            ejectedMag.transform.parent = transform;

            if (ejectedMag.transform.localPosition.y > -ClipSnapDistance)
            {
                ejectedMag.transform.localPosition = new Vector3(0, -0.1f, 0);
            }

            // Eject with physics force
            ejectedMag.transform.parent = null;
            ejectRigid.AddForce(-ejectedMag.transform.up * EjectForce, ForceMode.VelocityChange);

            yield return new WaitForFixedUpdate();
            ejectedMag.transform.parent = null;

        }

        yield return null;
    }



    public void EjectClip()
    {

        ammoController.RemoveClip();

        ejectEvent.Activate();
    }


    private void OnTriggerEnter(Collider other) {

        AmmoClip clip = other.GetComponent<AmmoClip>();
        


        if (clip != null) {
            // Weapon is full
            if( !ammoController.AddClip(clip) ) {
                return;
            };

            

            HeldMagazine = clip.grabbable;
            HeldMagazine.transform.parent = clipAttachmentPoint;
            HeldCollider = other;





            // Disable the collider while we're sliding it in to the weapon
            if (HeldCollider != null)
            {
                HeldCollider.enabled = false;
            }


            insertEvent.Activate();

        }
    }

}



*/