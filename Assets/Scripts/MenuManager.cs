using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //public int state;
    public bool tutorialScanAvailable;
    public bool tutorialBrokenScanAvailable;
    public bool tutorialRiverAvailable;

    public CanvasGroup Codex;
    public CanvasGroup Pause;
    public CanvasGroup MiniPause;
    public CanvasGroup ScannerTutorial;
    public CanvasGroup River;
    public CanvasGroup ScannerBrokenTutorial;

    public bool newInformation;
    // Start is called before the first frame update
    void Start()
    {
        ScannerTutorial.alpha = 1f;
        MiniPause.alpha = 0f;
    }

    public void InitMenu()
    {
        MiniPause.alpha = 1f;
        MiniPause.interactable = true;
        MiniPause.blocksRaycasts = true;
        ScannerTutorial.alpha = 0f;
    }
    public void PauseOpen()
    {
        //Debug.Log("PAUSE");
        MiniPause.alpha = 0f;
        MiniPause.interactable = false;
        MiniPause.blocksRaycasts = false;

        Pause.alpha = 1f;
        Pause.interactable = true;
        Pause.blocksRaycasts = true;

        FindObjectOfType<UiScanner>().OverrideScan = true;
        FindObjectOfType<GyroCamOffset>().lockMov = true;

    }

    public void PauseClose()
    {
        MiniPause.alpha = 1f;
        MiniPause.interactable = true;
        MiniPause.blocksRaycasts = true;

        Pause.alpha = 0f;
        Pause.interactable = false;
        Pause.blocksRaycasts = false;
        FindObjectOfType<UiScanner>().OverrideScan = false;
        FindObjectOfType<GyroCamOffset>().lockMov = false;
        FindObjectOfType<GyroCamOffset>().UserMovement = Vector3.Lerp(FindObjectOfType<GyroCamOffset>().UserMovement, Vector3.zero, Time.deltaTime);
    }
    public void OpenUiRiver()
    {
        MiniPause.alpha = 0f;
        MiniPause.interactable = false;
        MiniPause.blocksRaycasts = false;
        River.alpha = 1f;
        River.interactable = true;
        River.blocksRaycasts = true;
    }
    public void CloseUiRiver()
    {
        MiniPause.alpha = 1f;
        MiniPause.interactable = true;
        MiniPause.blocksRaycasts = true;
        River.alpha = 0f;
        River.interactable = false;
        River.blocksRaycasts = false;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenCodex()
    {
        //open codex

    }
}
