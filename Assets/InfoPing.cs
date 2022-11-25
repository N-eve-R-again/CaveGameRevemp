using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPing : MonoBehaviour
{

    public float minOffset;
    public float maxOffset;
    public bool revealed;
    public bool savedInfo;
    public scanner MainScanner;
    public GameObject pingUi;
    public GameObject Playercamera;
    public float distance;
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

        if(revealed)
        {
            pingUi.SetActive(true);
            pingUi.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else
        {
            if (savedInfo)
            {
                pingUi.SetActive(true);
                pingUi.GetComponent<MeshRenderer>().material.color = Color.cyan;
            }
            else
            {
                pingUi.SetActive(false);
                pingUi.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }

        }

    }
}
