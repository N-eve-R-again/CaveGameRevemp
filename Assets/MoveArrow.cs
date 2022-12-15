using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    public float minOffset;
    public float maxOffset;
    public bool revealed;
    public int dir;
    public scanner MainScanner;
    public GameObject arrowUi;
    public GameObject Playercamera;
    public GameManager gameManager;
    public float distance;
    //public InfoData data;
    //public InfoUiAnimation infoCanvas;
    public infoState currentState;
    public enum infoState
    {
        NotScanned,
        Saved
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
        maxOffset = minOffset + distance * (1f + minOffset * 2f);
        maxOffset = Mathf.Clamp(maxOffset, minOffset, 7.9f);
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

        arrowUi.SetActive(revealed);

        switch (currentState)
        {
            case infoState.NotScanned:
                arrowUi.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case infoState.Saved:
                arrowUi.SetActive(true);
                arrowUi.GetComponent<MeshRenderer>().material.color = Color.cyan;

                break;
            default:
                break;
        }

    }
    public void UseArrow()
    {
        Debug.Log("ARROW CLICK");
        gameManager.dir = dir;
        gameManager.StartFade();

    }
}
