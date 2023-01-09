using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerManager : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public float timerFlicker;
    public int flickerNumber = 5;
    public int remainingFlicker = 0;
    public float timeBeforeFlickerMax = 1.5f;
    public float timeBeforeFlickerMin = 0.8f;
    public float timeAfterFlickerMax = 0.3f;
    public float timeAfterFlickerMin = 0.1f;
    public bool flicker;
    public bool playSound;
    public bool flickerOnLoad;
    // Start is called before the first frame update
    void Start()
    {

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;


        if (flickerOnLoad)
        {
            StartFlicker();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (flicker)
        {
            if(timerFlicker < 0f)
            {
                if(remainingFlicker == flickerNumber && playSound)
                {

                   AudioManager.instance.PlayOS(-1);

                }
                if(remainingFlicker > 0)
                {
                    canvasGroup.alpha = canvasGroup.alpha * -1 + 1;
                    timerFlicker = Random.Range(0.01f *flickerNumber, 0.03f * flickerNumber);
                    remainingFlicker -= 1;
                    if(canvasGroup.alpha == 0)
                    {
                        canvasGroup.interactable = false;
                        canvasGroup.blocksRaycasts = false;
                    }
                    else
                    {
                        canvasGroup.interactable = true;
                        canvasGroup.blocksRaycasts = true;
                    }

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
        canvasGroup.alpha = 0;
        flicker = true;
        timerFlicker = Random.Range(timeBeforeFlickerMin, timeBeforeFlickerMax);
        remainingFlicker = flickerNumber;
    }

    public void StopFlicker()
    {
        canvasGroup.alpha = 1;
        flicker = true;
        timerFlicker = Random.Range(timeAfterFlickerMax, timeAfterFlickerMin);
        remainingFlicker = flickerNumber;
    }
}
