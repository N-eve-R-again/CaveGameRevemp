using System.Collections;
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
    public GameObject moreinfoButton;

    private int textnumbers;
    public bool readableFont;

    public void LoadDataOnScreen(InfoData tempData, bool corrupted)
    {
        FindObjectOfType<CodexManager>().locked[tempData.codexIndex] = false;
        textnumbers = texts.Length;
        for (int i = 0; i < textnumbers; i++)
        {
            texts[i].Key = tempData.texts[i];
            texts[i].Localize();

            if (corrupted)
            {
                Debug.Log("loadCorrupt");
                TextMeshProUGUI temp = texts[2].GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI temp2 = texts[1].GetComponent<TextMeshProUGUI>();
                string temptext = temp.text;
                string result = "";
                for (int x = 0; x < temptext.Length; x++)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        result += temptext[x];

                    }
                    else
                    {
                        result += '#';
                    }
                }
                temp.text = result;
                temp2.text = result;
                moreinfoButton.SetActive(false);
            }
            else
            {
                moreinfoButton.SetActive(true);
            }

        }
        for (int i = 0; i < flickers.Length -2; i++)
        {
            flickers[i].StartFlicker();
        }



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
