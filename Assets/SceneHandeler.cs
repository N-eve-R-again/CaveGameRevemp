using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandeler : MonoBehaviour
{
    public GameObject Mesh;
    public Transform CameraPivot;
    public GameObject ParticleSystem;
    public GyroCamOffset cameraManager;
    public InfoPing[] Pings;
    // Start is called before the first frame update
    void Start()
    {
        //HideScene();
    }

    // Update is called once per frame
    public void ShowScene()
    {
        Mesh.SetActive(true);
        ParticleSystem.SetActive(true);
        cameraManager.UpdatePosition(CameraPivot);

        for (int i = 0; i < Pings.Length; i++)
        {
            Pings[i].gameObject.SetActive(true);
        }
        Debug.Log("SHOW SCENE");
    }
    public void HideScene()
    {
        Mesh.SetActive(false);
        ParticleSystem.SetActive(false);
        for (int i = 0; i < Pings.Length; i++)
        {
            Pings[i].gameObject.SetActive(false);
        }
        Debug.Log("Hide SCENE");
    }
}
