using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
#############################
# TOOL MADE BY GEMINITAURUS #
#############################
*/
public class ChangeLanguage : MonoBehaviour
{
    public MainMenuManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ButtonLanguage(int index)//On Button Click
    {
        AudioManager.instance.PlayOS(2);
        LocalizationSystem.instance.currentLanguage = index;
        LocalizationSystem.instance.Init();
        //arrow.SetActive(true); 
        //arrowdeselect.SetActive(false);

        StartCoroutine("LoadingNewLanguage");//wait before reloading scene
    }

    private IEnumerator LoadingNewLanguage()
    {
        while (!LocalizationSystem.instance.isReady)
        {
            yield return null;//wait
        }
        manager.startButton.GetComponentInChildren<LocalizedText>().Localize();
        manager.startButton.GetComponent<FlickerManager>().StartFlicker();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//reload scene

    }
}
