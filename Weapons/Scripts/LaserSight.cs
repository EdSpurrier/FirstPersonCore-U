using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    [InlineButton("Setup")]
    public bool active = false;
    public UpdateType updateType = UpdateType.LateUpdate;

    [Title("Parts")]
    public LineRenderer lineRenderer;
    public Transform startPoint;
    public Transform endPoint;
    

    void Setup()
    {
        Refresh();
    }

    [PropertySpace(SpaceBefore = 30, SpaceAfter = 30)]
    [Button("Refresh / Setup", ButtonSizes.Medium), GUIColor(0.5f, 0.95f, 0.4f)]
    private void Refresh()
    {
        if (!startPoint)
        {
            GameObject newPoint = new GameObject();
            newPoint.name = "Start Point";
            startPoint = newPoint.transform;
            startPoint.parent = transform;

            startPoint.position = transform.position;
            startPoint.rotation = transform.rotation;
        };

        if (!endPoint)
        {
            GameObject newPoint = new GameObject();
            newPoint.name = "End Point";
            endPoint = newPoint.transform;
            endPoint.parent = transform;
            endPoint.rotation = transform.rotation;
            endPoint.position = transform.position + (transform.forward * 2f);
        };

        Vector3[] linePoints = new Vector3[2] { 
            startPoint.position,
            endPoint.position
        };

        lineRenderer.SetPositions(linePoints);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    /// <summary>
    /// Execute methods responsible for component's behaviour
    /// </summary>
    void LateUpdate()
    {
        if (updateType != UpdateType.LateUpdate) return;
        UpdateMethods();
    }

    void Update()
    {
        if (updateType != UpdateType.Update) return;
        UpdateMethods();
    }

    void FixedUpdate()
    {
        if (updateType != UpdateType.FixedUpdate) return;
        UpdateMethods();
    }


    void UpdateMethods()
    {
        if (active)
        {
            Aim();
        };
    }



    void Aim()
    {
        Vector3[] linePoints = new Vector3[2] {
            startPoint.position,
            endPoint.position
        };

        lineRenderer.SetPositions(linePoints);
    }
}
