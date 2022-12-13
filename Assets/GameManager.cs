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

    bool dir;
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
            if (currentScene < scenes.Length - 1)
            {
                currentScene += 1;

                ActualizeScene();
                mscanner.timer = 0.99f;
                QueueChanging = false;
            }
        }
    }

    public void StartFade()
    {
        if (currentScene < scenes.Length - 1)
        {
            fadeManager.GetComponent<Animator>().Play("FadeToBlack");
            QueueChanging = true;
        }
    }
    public void ActualizeScene()
    {
        scenes[currentScene].ShowScene();
        scenes[currentScene - 1].HideScene();
    }
}
