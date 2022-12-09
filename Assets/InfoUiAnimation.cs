using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoUiAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI[] texts;
    public Image image;
    public Animator animationManager;
    public void LoadDataOnScreen(InfoData tempData)
    {
        for (int i = 0; i < tempData.texts.Length; i++)
        {
            texts[i].SetText(tempData.texts[i]);
        }
        image.sprite = tempData.artwork;
        animationManager.Play(tempData.loadAnimation.name);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animationManager.Play("Exit");
            FindObjectOfType<UiScanner>().OverrideScan = false;

        }
    }
}
