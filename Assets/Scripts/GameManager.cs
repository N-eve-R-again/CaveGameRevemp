using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneHandeler[] scenes;
    public int currentScene;
    public UiScanner uiscanner;
    public scanner mscanner;
    public FadeManager fadeManager;
    public bool QueueChanging;
    public int dir;
    // Start is called before the first frame update
    public void LateStart()
    {

        
        AudioManager.instance.PlayLP(0);
        scenes[0].ShowScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeManager.Fading && QueueChanging)
        {

            if (currentScene < scenes.Length - dir)
            {
                currentScene += dir;

                ActualizeScene();
                mscanner.timer = 0.99f;
                mscanner.currentoffset = 9f;
                QueueChanging = false;
            }
        }
    }

    public void StartFade()
    {
        if (currentScene < scenes.Length - dir)
        {
            fadeManager.GetComponent<Animator>().Play("FadeToBlack");
            QueueChanging = true;
            
        }
    }
    public void ActualizeScene()
    {
        scenes[currentScene].ShowScene();
        if (scenes[currentScene].riverScene)
        {
            uiscanner.OverrideScan = true;
        }
        else
        {
            uiscanner.OverrideScan = false;
        }
        scenes[currentScene - dir].HideScene();
    }

    public void StartRiverLevel()
    {

    }
}
