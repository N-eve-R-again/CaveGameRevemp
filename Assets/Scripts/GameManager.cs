using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneHandeler[] scenes;
    public int currentScene;
    public scanner mscanner;
    public FadeManager fadeManager;
    public bool QueueChanging;
    public int dir;
    // Start is called before the first frame update
    void Start()
    {
        scenes[0].ShowScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeManager.Fading && QueueChanging)
        {
            Debug.Log("QUEUE CHANGE");
            if (currentScene < scenes.Length - dir)
            {
                currentScene += dir;

                ActualizeScene();
                mscanner.timer = 0.99f;
                QueueChanging = false;
            }
        }
    }

    public void StartFade()
    {
        if (currentScene < scenes.Length - dir)
        {
            Debug.Log("ANIM");
            fadeManager.GetComponent<Animator>().Play("FadeToBlack");
            QueueChanging = true;
        }
    }
    public void ActualizeScene()
    {
        Debug.Log("CCCCCCCCCCCCCCC");
        scenes[currentScene].ShowScene();
        scenes[currentScene - dir].HideScene();
    }
}