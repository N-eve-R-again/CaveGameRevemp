using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRiver : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 1f;

    public float alpha;
    public CanvasGroup image;
    public void CloseTutorial()
    {
        if(timer <= 0f)
        {
            gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha < 0.2f)
        {
            alpha = Mathf.Lerp(alpha, 0.5f, Time.deltaTime);
            image.alpha = alpha;
        }
        else
        {
            alpha = 0f;
        }

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }
}
