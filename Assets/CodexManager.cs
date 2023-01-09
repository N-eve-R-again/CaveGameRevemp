using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodexManager : MonoBehaviour
{
    public int index;
    public InfoData[] infos;
    public bool[] locked;

    public LocalizedText[] texts;
    public FlickerManager[] flickers;
    public Image image;
    public GameObject[] switchFontButtonTexts;
    public bool readableFont;
    public int cacheindex;
    public Sprite lockedInfoSprite;
    public TextMeshProUGUI indexText;
    public Slider sliderInterface;
    // Start is called before the first frame update
    void Start()
    {
        sliderInterface.minValue = -1;
        sliderInterface.maxValue = infos.Length;
    }

    // Update is called once per frame
    public void OpenPage()
    {
        FindObjectOfType<UiScanner>().OverrideScan = true;
        for (int i = 0; i < flickers.Length - 3; i++)
        {
            flickers[i].StartFlicker();
        }
    }    
    public void ClosePage()
    {
        FindObjectOfType<UiScanner>().OverrideScan = false;
        for (int i = 0; i < flickers.Length - 3; i++)
        {
            flickers[i].StopFlicker();
        }
    }

    public void UpdateSlider(int value)
    {
        sliderInterface.value = value;
        indexText.text = (value+1).ToString();
    }

    public void ChangeInfo(int dir)
    {
        //Debug.Log(index + dir);
         


        index += dir;

        if (index == infos.Length)
        {
            index = 0;
        }
        else if (index == -1)
        {
            index = infos.Length - 1;
        }

        if (locked[index])
        {
            LockedInfo();
            image.sprite = lockedInfoSprite;
        }
        else
        {
            texts[0].Key = infos[index].texts[0];
            texts[1].Key = infos[index].texts[3];
            texts[2].Key = infos[index].texts[3];
            image.sprite = infos[index].artwork;
        }
        UpdateSlider(index);

        texts[0].Localize();
        texts[1].Localize();
        texts[2].Localize();
        
        for (int i = flickers.Length -3; i < flickers.Length-1; i++)
        {
            flickers[i].StartFlicker();
        }
        
    }

    public void LockedInfo()
    {
        texts[0].Key = "info_locked";
        texts[1].Key = "info_locked";
        texts[2].Key = "info_locked";
    }
    public void SwitchFont()
    {
        if (!readableFont)
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
