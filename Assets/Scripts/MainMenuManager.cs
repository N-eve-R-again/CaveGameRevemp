using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.Intrinsics;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public float letterPause;
    public BootContent bootContent;
    public int maxLines;
    //int lineMoved = 1;
    public float moveFactor;
    public bool ready;
    public int currentScreen;
    public GameObject startButton;
    public FlickerManager TitleDrop;
    public int impact;
    
    

    public GameObject[] languageButtons;
    
    // Start is called before the first frame update
    void Start()
    {
        //textDisplay = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TypeSentence(bootContent.content));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ready && Input.GetMouseButtonDown(0))
        {
            ready = false;
            //menu de langue
            textDisplay.enabled = false;
            currentScreen = 1;
            UpdateUi();
        }
        if (currentScreen == 0&& Input.GetMouseButtonDown(0))
        {
            letterPause = letterPause / 4f;
        }
    }
    void UpdateUi()
    {
        switch (currentScreen)
        {
            case 1:
                languageButtons[0].GetComponent<FlickerManager>().StartFlicker();
                languageButtons[1].GetComponent<FlickerManager>().StartFlicker();

                AudioManager.instance.PlayOS(1);
                break; 

            case 2:
                languageButtons[0].GetComponent<FlickerManager>().StopFlicker();
                languageButtons[1].GetComponent<FlickerManager>().StopFlicker();
                startButton.GetComponent<FlickerManager>().StopFlicker();
                StartCoroutine(StartGame());
                break;
        }
    }

    IEnumerator TypeSentence(string[] sentence)
    {

        textDisplay.text = "";
        AudioManager.instance.PlayOS(0);
        AudioManager.instance.PlayOS(5);
        yield return new WaitForSeconds(1f);
        AudioManager.instance.PlayOS(1);
        textDisplay.text = sentence[0];
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 1; i < sentence.Length; ++i)
        {

            yield return new WaitForSeconds(letterPause * Random.Range(1f,1.8f));
            if(i < maxLines)
            {
                textDisplay.text += "\n" + sentence[i];
            }
            else
            {
                textDisplay.text += "\n" + sentence[i];
                textDisplay.rectTransform.position -= new Vector3(0, moveFactor, 0);
            }
            if(i == impact)
            {
                AudioManager.instance.FadeOS(5);
                AudioManager.instance.PlayOS(4);
                yield return new WaitForSeconds(0.2f);
                string temp = textDisplay.text;
                textDisplay.text = "";
                for (int x = 0; x < temp.Length * 5; x++)
                {
                    
                    if (Random.Range(0, 2) == 0)
                    {
                        textDisplay.text += "@@@@";
                    }
                    else
                    {
                        textDisplay.text += "####";
                    }
                }
                textDisplay.color = Color.red;
                yield return new WaitForSeconds(0.2f);
                textDisplay.color = Color.white;
                textDisplay.text = temp;

            }
            AudioManager.instance.PlayOS(2);

        }
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.FadeOS(0);
        AudioManager.instance.PlayOS(3);
        ready = true;
    }
    public void StartButton()
    {
        currentScreen = 2;
        AudioManager.instance.PlayOS(1);
        UpdateUi();
    }
    IEnumerator StartGame()
    {
        
        yield return new WaitForSeconds(1f);
        TitleDrop.StartFlicker();
        yield return new WaitForSeconds(5f);
        TitleDrop.StopFlicker();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
