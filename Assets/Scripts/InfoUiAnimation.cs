using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoUiAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public LocalizedText[] texts;
    
    public FlickerManager[] flickers;
    public GameObject[] switchFontButtonTexts;
    public Image image;

    private int textnumbers;
    public bool readableFont;

    public void LoadDataOnScreen(InfoData tempData)
    {
        textnumbers = texts.Length;
        for (int i = 0; i < textnumbers; i++)
        {
            texts[i].Key = tempData.texts[i];
            texts[i].Localize();
        }
        for (int i = 0; i < flickers.Length -2; i++)
        {
            flickers[i].StartFlicker();
        }
        image.sprite = tempData.artwork;



    }

    public void CloseInfo()
    {

        FindObjectOfType<UiScanner>().OverrideScan = false;
        for (int i = 0; i < flickers.Length - 2; i++)
        {
            flickers[i].StopFlicker();
        }


    }

    
    public void SwitchFont()
    {
        if(!readableFont)
        {
            flickers[flickers.Length - 1].StartFlicker();
            flickers[flickers.Length - 2].StopFlicker();
            switchFontButtonTexts[1].SetActive(true);
            switchFontButtonTexts[0].SetActive(false);

            
        }
        else
        {
            flickers[flickers.Length - 1].StopFlicker();
            flickers[flickers.Length - 2].StartFlicker();
            switchFontButtonTexts[1].SetActive(false);
            switchFontButtonTexts[0].SetActive(true);
        }
        readableFont = !readableFont;

    }
}
