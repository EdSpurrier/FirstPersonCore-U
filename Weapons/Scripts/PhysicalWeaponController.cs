/*using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalWeaponController : MonoBehaviour
{
    [Title("Weapon Settings")]
    [HideLabel]
    public Damager damage;
    public ImpactType impactType;

    [Title("This Needs To Connect To Speed")]
    public float damageMultiplier = 1f;

    [Title("Trigger Settings")]
    public LayerMaskNames layerMask;

    [Title("Hit Trigger")]
    [HideLabel]
    public FrameCoreEvent hitEvent;


    [Title("Hit Objects")]
    [HideLabel]
    public ListOfObjects hitObjects;


    [Title("Parts")]
    public Collider coll;


    [Title("System")]
    [HideLabel]
    public DeBugger debug;





    //  HOW:
    //
    //  Turn Into Actual Colldier
    //  Collider Does Not Collide with set colliders
    //  Collision is tracked & get hit point
    //  Get Hit Point for impact spawn & attach
    //  DONT USE A TRIGGER FOR THIS???



    public void Setup()
    {
        if (EditorInteractions.InEditorButton())
        {

            coll = GetComponent<Collider>();

            if (!coll)
            {
                Debug.LogError("No Collider Attached! Please Attach & Connect One!");
            };

        };
    }

    public bool CheckObjectInTriggerMask(GameObject thisGameObject)
    {
        bool result = false;

        if (Frame.core.layerMasks.InLayerMask(layerMask, thisGameObject))
        {
            debug.Log("Impact With Object Allowed = " + thisGameObject.name);
            if (type.IsTriggerAllowed())
            {
                debug.Log("Trigger Allowed");
                result = true;
            };
        }
        else
        {
            debug.Log("No Impact With Object = " + thisGameObject.name);
        };

        return result;
    }



    //  COLLISIONS
    private void OnCollisionEnter(Collision collision)
    {

            if (coll != collision.contacts[0].thisCollider)
            {
                Debug.Log("Not This Collider");
                return;
            };


        GameObject collisionObject = collision.contacts[0].thisCollider.gameObject;

        if (CheckObjectInTriggerMask(collisionObject))
        {
            hitObjects.AddGameObject(collisionObject);

            hitPoint.position = collision.contacts[0].point;
            hitPoint.rotation = Quaternion.LookRotation(collision.contacts[0].normal);

            enterEvent.Activate();
        };
    }

    private void OnCollisionExit(Collision coll)
    {
        if (impactColliderOnly)
        {
            if (coll != collision.contacts[0].thisCollider)
            {
                Debug.Log("Not This Collider");
                return;
            };
        };

        GameObject collisionObject = collision.gameObject;


        if (CheckObjectInTriggerMask(collisionObject))
        {
            hitObjects.RemoveGameObject(collisionObject);
        };
    }










    public void DamageObject (GameObject hitObject)
    {
        // WORK HERE
        //damage.Damage(hitObject, gameObjectWithHitPointAndFloat.floatValue);

        //Frame.core.substances.ImpactGameObject(gameObjectWithHitPointAndFloat.gameObject, impactType, gameObjectWithHitPointAndFloat.position, gameObjectWithHitPointAndFloat.rotation);

        debug.Log("Damage Triggered");
    }

}
*/