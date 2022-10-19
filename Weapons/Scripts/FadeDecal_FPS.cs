/*using UnityEngine;
using System.Collections;

public class FadeDecal_FPS : MonoBehaviour
{
    public bool active = false;

    public string shaderProperty = "_TintColor";
    public float fadeSpeed = 1;

    public int propertyID;
    public Color currentColor;

    public MaterialPropertyBlock props;
    public Renderer rend;
    public Material material;

    public bool debugSlider = false;
    [Range(0.0f, 1.0f)]
    public float transition = 1f;

    void Awake()
    {
        if (props == null) props = new MaterialPropertyBlock();
        if (rend == null) rend = GetComponent<Renderer>();

        propertyID = Shader.PropertyToID(shaderProperty);

        
        rend.GetPropertyBlock(props);
    }


    public void Activate()
    {
        active = true;
    }

    private void Update()
    {
        if (!active)
        {
            return;
        };

        currentColor.a = transition;
        material.color = currentColor;
    }

}*/