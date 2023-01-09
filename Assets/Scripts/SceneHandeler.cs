using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandeler : MonoBehaviour
{

    public Transform CameraPivot;
    public GyroCamOffset cameraManager;
    //public GameObject[] Objects;
    public Transform riverModePlayer;

    public bool riverScene;
    public bool containsWater;
    // Start is called before the first frame update
    void Start()
    {
        //HideScene();
        
    }

    // Update is called once per frame
    public void ShowScene()
    {
        cameraManager.UpdatePosition(CameraPivot);
        if (containsWater)
        {
            AudioManager.instance.PlayLP(1);
            //Debug.Log("contains water sound");
        }
        else
        {
            AudioManager.instance.FadeLP(1);
        }

        if (riverScene)
        {
            cameraManager.targetTransform = riverModePlayer;
            cameraManager.targetMov = true;
            AudioManager.instance.PlayLP(2);
            FindObjectOfType<MenuManager>().OpenUiRiver();
        }
        else
        {
            AudioManager.instance.FadeLP(2);
            cameraManager.targetMov = false;
            if (!FindObjectOfType<MenuManager>().tutorialScanAvailable)
            {
                FindObjectOfType<MenuManager>().CloseUiRiver();
            }

        }


        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            //Objects[i].SetActive(true);
        }
        //transform.childCount;
        
    }
    public void HideScene()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            //Objects[i].SetActive(false);
            transform.GetChild(i).gameObject.SetActive(false);
        }


    }

}
