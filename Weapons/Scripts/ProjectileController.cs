using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class ProjectileController : MonoBehaviour
{
    [Title("Stats")]
    public float maximumDistance = Mathf.Infinity;
    public float lifeTime = Mathf.Infinity;
    public float speed = 50f;


    [Title("Raycast Settings")]
    public LayerMaskNames layerMask = LayerMaskNames.Weapon;
    [InlineButton("Setup")]
    public Transform origin;
    public Transform hitPoint;
    public ListOfObjects hitObjects;

    public void Setup()
    {
        if (EditorInteractions.InEditorButton())
        {
            if (!hitPoint)
            {
                hitPoint = transform.FindChildWithName("Hit Point");
            };

            if (!hitPoint)
            {
                Debug.Log("No Hit Point Transform Found!");
                Debug.Log("Creating New Hit Point Transform...");
                GameObject newHitPoint = new GameObject();
                newHitPoint.name = "Hit Point";
                newHitPoint.transform.parent = transform;
                newHitPoint.transform.localPosition = Vector3.zero;
                newHitPoint.transform.localRotation = Quaternion.identity;
                hitPoint = newHitPoint.transform;
            };

            if (!origin)
            {
                origin = transform;
            };

            hitObjects = GetComponent<ListOfObjects>();
            if (!hitObjects)
            {
                hitObjects = gameObject.AddComponent<ListOfObjects>();
            };
            hitObjects.listName = "Hit Objects With Hit Point & Float";
            hitObjects.listType = ListType.GameObjectsWithHitPointAndFloat;
        };
    }




    [Title("Bounce")]
    public bool bounceActive = false;
    
    [ShowIfGroup("bounceActive")]
    [FoldoutGroup("bounceActive/Bounce")]
    [Range(0, 10)] public int bounceLimitMin = 1;
    [FoldoutGroup("bounceActive/Bounce")]
    [Range(1, 10)] public int bounceLimitMax = 5;
    [FoldoutGroup("bounceActive/Bounce")]
    [Range(0f, 1f)] public float bounceThreshold = 0.5f;
    [FoldoutGroup("bounceActive/Bounce")]
    [Range(0f, 1f)] public float bounceChance = 0.5f;
    [FoldoutGroup("bounceActive/Bounce")]
    public float thresholdByNormal = 0.05f;
    [FoldoutGroup("bounceActive/Bounce/Bounce Event")]
    [HideLabel]
    public FrameCoreEvent bounceEvent = new FrameCoreEvent
    {
        eventName = "Bounce"
    };


    [Title("Event Settings")]
    public bool hitEventOnDistanceReached = true;
    public bool timeOrDistanceReachedEventActive = false;


    [Title("Events")]
    [FoldoutGroup("Hit Event")]
    [HideLabel]
    public FrameCoreEvent hitEvent = new FrameCoreEvent
    {
        eventName = "Hit"
    };


    
    
    [ShowIfGroup("timeOrDistanceReachedEventActive")]

    [FoldoutGroup("timeOrDistanceReachedEventActive/Distance Reached Event")]
    [HideLabel]
    public FrameCoreEvent timeDistanceReachedEvent = new FrameCoreEvent
    {
        eventName = "Time or Distanced Reached"
    };


 


    [Space]

    [FoldoutGroup("System")]
    public bool hitComplete = false;
    [FoldoutGroup("System")] 
    public int hits = 0;
    [FoldoutGroup("System")]
    public int bounceLimit = 0;
    [FoldoutGroup("System")]
    public float distance;
    [FoldoutGroup("System")]
    public float elapsedTime = 0;

    [HideLabel]
    public DeBugger debug;





    private RaycastHit hit;
    


    private void OnEnable()
    {
        if (origin == null)
        {
            origin = transform;
        };


        hitComplete = false;
        bounceLimit = Random.Range(bounceLimitMin, bounceLimitMax);
        hits = 0;
    }




    private void Update()
    {
        if(hitComplete)
        {
            return;
        };


        elapsedTime += Time.deltaTime;

        if (elapsedTime >= lifeTime)
        {
            TimeOrDistanceReached();
        };
    }

    private void FixedUpdate()
    {
        if (hitComplete)
        {
            return;
        };


        UpdateProjectile(Time.deltaTime);
    }

    private void UpdateProjectile(float deltaTime)
    {


        float deltaPosition = deltaTime * speed;

        

        distance += deltaPosition;

        if (distance >= maximumDistance)
        {
            TimeOrDistanceReached();
            return;
        }

        transform.position += deltaPosition * transform.forward;





        Ray ray = new Ray(origin.position, origin.forward);

        if (Physics.Raycast(ray, out hit, deltaPosition, Frame.core.layerMasks.GetLayerMask(layerMask)))
        {
            if (Frame.core.layerMasks.InLayerMask(layerMask, hit.collider.gameObject))
            {
                Debug.DrawLine(origin.position, hit.point, Color.green);
                hitPoint.position = hit.point;


                float dotResult = Vector3.Dot(transform.forward, hit.normal);

                bool canBounce = Mathf.Abs(dotResult) <= bounceThreshold;
                bool randomBounce = Random.value <= bounceChance;

                if (!bounceActive || hits >= bounceLimit || !canBounce || !randomBounce)
                {
                    float hitMultiplier = 1f;

                    Vector3 inverseDirection = origin.position - hitPoint.position;

                    hitObjects.AddGameObjectWithHitPointAndFloat(hit.collider.gameObject, hitMultiplier, hit.point, Quaternion.LookRotation(inverseDirection));

                    hitComplete = true;

                    Hit();
                }
                else
                {
                    Vector3 newForward = Vector3.Reflect(transform.forward, hit.normal);
                    transform.position = hit.point + newForward;

                    transform.rotation = Quaternion.LookRotation(newForward);

                    Bounce();
                }
                hits++;

            };
        };


    }




    void TimeOrDistanceReached()
    {
        if (timeOrDistanceReachedEventActive)
        {
            timeDistanceReachedEvent.Activate();
        };

        if (hitEventOnDistanceReached)
        {
            hitEvent.Activate();
        };
    }

    void Hit()
    {
        hitEvent.Activate();
    }

    void Bounce()
    {
        bounceEvent.Activate();
    }
}
