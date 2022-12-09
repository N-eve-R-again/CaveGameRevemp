using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerManager : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public float timerFlicker;
    public int flickerNumber = 5;
    public bool flicker;
    // Start is called before the first frame update
    void Start()
    {
        //StartFlicker();
    }

    // Update is called once per frame
    void Update()
    {
        if (flicker)
        {
            if(timerFlicker < 0f)
            {
                if(flickerNumber > 0)
                {
                    canvasGroup.alpha = canvasGroup.alpha * -1 + 1;
                    timerFlicker = Random.Range(0.01f *flickerNumber, 0.03f * flickerNumber);
                    flickerNumber -= 1;
                }
                else
                {
                    flicker = false;
                }
            }
            else
            {
                timerFlicker -= Time.deltaTime;
            }


        }
    }

    public void StartFlicker()
    {
        flicker = true;
        timerFlicker = Random.Range(0.8f, 1.5f);
        flickerNumber = 5;
    }

    public void StopFlicker()
    {

        flicker = true;
        timerFlicker = Random.Range(0.1f, 0.3f);
        flickerNumber = 5;
    }
}
