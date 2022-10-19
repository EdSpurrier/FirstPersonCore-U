using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractableSelector
{
    public LayerMaskNames layerMask = LayerMaskNames.PlayerPoint;
    public float pointDistance = 3f;
    public Transform originTransform;


    [FoldoutGroup("System")]
    public InteractableInput selectedInput;
    [FoldoutGroup("System")] 
    public Vector3 pointPoint = Vector3.zero;

    [FoldoutGroup("System")]
    public bool active = true;

    [FoldoutGroup("System")]
    [Title("Debug")]
    public bool debugHitPoint = false;
    [FoldoutGroup("System")]
    public Transform debugHitPointTransform;
    [FoldoutGroup("System")]
    [HideLabel]
    public DeBugger debug;

    private RaycastHit hit;
    bool debugHitPointShown = false;

    public void TriggerInteractable()
    {
        if (!active)
        {
            return;
        };
     
        if (selectedInput)
        {
            selectedInput.Trigger();
        };
    }


    public void UnTriggerInteractable()
    {
        if (!active)
        {
            return;
        };

        if (selectedInput)
        {
            selectedInput.UnTrigger();
        };
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        if (selectedInput)
        {
            selectedInput.selectable.Deselect();
        };
        
        selectedInput = null;
        active = false;
    }

    public void Update()
    {
        if (!active)
        {
            return;
        };

        Point();
    }


    void Point()
    {
        Vector3 fwd = originTransform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(originTransform.position, fwd, out hit, pointDistance, Frame.core.layerMasks.GetLayerMask(layerMask)))
        {
            Debug.DrawLine(originTransform.position, hit.point, Color.green);
            pointPoint = hit.point;

            InteractableInput newSelectedInput = hit.collider.gameObject.GetComponent<InteractableInput>();

            if (newSelectedInput != selectedInput)
            {
                if (selectedInput)
                {
                    selectedInput.selectable.Deselect();
                };
                

                if (newSelectedInput)
                {
                    selectedInput = newSelectedInput;
                    selectedInput.selectable.Select();
                }
                else {
                    selectedInput = null;
                };
            };

        }
        else
        {
            Debug.DrawRay(originTransform.position, fwd * pointDistance, Color.red);
            pointPoint = originTransform.position + (fwd * pointDistance);

            if (selectedInput)
            {
                selectedInput.selectable.Deselect();
                selectedInput = null;
            };
        };

        DebugHitPoint();
    }

    void DebugHitPoint ()
    {
        if (debugHitPoint && !debugHitPointShown)
        {
            debugHitPointTransform.gameObject.SetActive(true);
            debugHitPointShown = true;
        }
        else if (!debugHitPoint && debugHitPointShown)
        {
            debugHitPointTransform.gameObject.SetActive(false);
            debugHitPointShown = false;
        };

        debugHitPointTransform.position = pointPoint;
    }


}
