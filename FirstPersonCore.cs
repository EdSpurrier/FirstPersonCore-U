using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FPC
{
    public static FirstPersonCore core;
}
public class FirstPersonCore : MonoBehaviour
{

    [Title("Cores")]
    [InlineButton("ConnectCores")]
    public FirstPersonPlayerCore player;


    bool systemError = false;




    private void ConnectCores()
    {
        if (EditorInteractions.InEditorButton())
        {
            player = CheckArrayForComponent(FindObjectsOfType(typeof(FirstPersonPlayerCore)) as FirstPersonPlayerCore[]) as FirstPersonPlayerCore;

            EditorInteractions.SetDirty(this);
        };
    }

    Component CheckArrayForComponent(Component[] components)
    {
        if (components.Length > 1 || components.Length == 0)
        {
            Debug.LogError("FirstPersonCore [ERROR] >> " + components.Length + " - " + components.GetType() + " Found!");
            return null;
        }
        else
        {
            Debug.Log("FirstPersonCore >> Connected " + components.GetType());
            return components[0];
        };
    }



    void CheckComponent(Component component)
    {
        if (!component)
        {
            Debug.LogError("FirstPersonCore [ERROR] >> Core Component Is Not Attached!");
            systemError = true;
        };
    }


    void CheckSetup()
    {
        CheckComponent(player);


        if (systemError)
        {
            Debug.LogError("FirstPersonCore Incorrectly Setup....");
            Debug.Break();
        };
    }


    //  CONNECT RUNTIME SCENE CORES
    void ConnectRuntimeCores()
    {
        //exampleRuntimeCore = CheckArrayForComponent(FindObjectsOfType(typeof(ExampleRuntimeCore)) as ExampleRuntimeCore[]) as ExampleRuntimeCore;
    }


    private void Awake()
    {
        FPC.core = this;

        ConnectRuntimeCores();

        CheckSetup();

        Debug.Log("FirstPersonCore Started...");

    }




    private void Update()
    {

    }

    private void FixedUpdate()
    {

    }


    private void LateUpdate()
    {

    }









}
