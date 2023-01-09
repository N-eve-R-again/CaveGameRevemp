using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPing : MonoBehaviour
{

    public float minOffset;
    public float maxOffset;
    public bool revealed;

    public scanner MainScanner;
    public GameObject pingUi;
    public GameObject Playercamera;
    public float distance;
    public InfoData data;
    public InfoUiAnimation infoCanvas;
    public infoState currentState;
    public enum infoState
    {
        NotScanned,
        Saved,
        Corrupted
    }
    // Start is called before the first frame update
    void Start()
    {
        MainScanner = FindObjectOfType<scanner>();
        CalculateOffsets();
    }
    void CalculateOffsets()
    {
        distance = Vector3.Distance(Playercamera.transform.position, transform.position);
        minOffset = distance / 10f;
        maxOffset = minOffset + distance * (1f + minOffset*2f);
        maxOffset = Mathf.Clamp(maxOffset, minOffset, 5.9f);
    }
    // Update is called once per frame
    void Update()
    {
        CalculateOffsets();
        if (MainScanner.currentoffset > minOffset && MainScanner.currentoffset < maxOffset)
        {
            revealed = true;
        }
        else
        {
            revealed = false;
        }

        pingUi.SetActive(revealed);

        switch (currentState)
        {
            case infoState.NotScanned:
                pingUi.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case infoState.Saved:
                pingUi.SetActive(true);
                pingUi.GetComponent<MeshRenderer>().material.color = Color.cyan;

                break;
            case infoState.Corrupted:
                pingUi.SetActive(true);
                pingUi.GetComponent<MeshRenderer>().material.color = Color.green;

                break;
            default:
                break;
        }

    }
    public void ShowInfo()
    {
        infoCanvas = FindObjectOfType<InfoUiAnimation>();
        if(currentState == infoState.Corrupted)
        {
            infoCanvas.LoadDataOnScreen(data, true);
        }
        else
        {
            infoCanvas.LoadDataOnScreen(data, false);
        }
        

    }
}
