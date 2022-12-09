using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoUiAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI[] texts;
    public FlickerManager[] flickers;
    public Image image;
    public Animator animationManager;
    private int textnumbers;
    public void LoadDataOnScreen(InfoData tempData)
    {
        textnumbers = tempData.texts.Length;
        for (int i = 0; i < textnumbers; i++)
        {
            texts[i].SetText(tempData.texts[i]);
        }
        for (int i = 0; i < textnumbers; i++)
        {
            flickers[i].StartFlicker();
        }
        image.sprite = tempData.artwork;
        image.GetComponent<FlickerManager>().StartFlicker();
        animationManager.Play("Enter");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animationManager.Play("Exit");
            FindObjectOfType<UiScanner>().OverrideScan = false;
            for (int i = 0; i < textnumbers; i++)
            {
                flickers[i].StopFlicker();
            }
            image.GetComponent<FlickerManager>().StopFlicker();
        }
    }
}
