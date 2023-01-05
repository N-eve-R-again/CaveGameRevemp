using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandeler : MonoBehaviour
{

    public Transform CameraPivot;
    public GyroCamOffset cameraManager;
    public GameObject[] Objects;
    public Transform riverModePlayer;

    public bool riverScene;
    // Start is called before the first frame update
    void Start()
    {
        //HideScene();
    }

    // Update is called once per frame
    public void ShowScene()
    {
        cameraManager.UpdatePosition(CameraPivot);
        if(riverScene)
        {
            cameraManager.targetTransform = riverModePlayer;
            cameraManager.targetMov = true;
        }
        else
        {
            cameraManager.targetMov = false;
        }


        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].SetActive(true);
        }
    }
    public void HideScene()
    {

        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].SetActive(false);
        }

    }

}
