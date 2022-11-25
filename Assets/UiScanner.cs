using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScanner : MonoBehaviour
{
    public float scannerWaitTime;
    public float scannerWaitTimer;

    public bool CanScan;
    public GameObject UiObject;
    public scanner MainScanner;
    public bool alreadyScan;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        for (var i = 0; i < Input.touchCount; i++)
        {
            //Debug.Log(Input.GetTouch(i).phase);
            if (Input.GetTouch(i).phase != TouchPhase.Ended && CanScan)
            {
                scannerWaitTimer += Time.deltaTime;
                UiObject.transform.localScale = new Vector3(scannerWaitTimer * 3, scannerWaitTimer * 3, 1f);
                UiObject.transform.position = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, UiObject.transform.position.z);
                if (scannerWaitTimer > scannerWaitTime)
                {
                    MainScanner.StartScan();
                    alreadyScan = true;
                    CanScan = false;
                    UiObject.transform.localScale = new Vector3(0f, 0f, 1f);

                }

                

            }
            else if(Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                scannerWaitTimer = 0f;
                alreadyScan = false;
                CanScan = true;
                UiObject.transform.localScale = new Vector3(0f, 0f, 1f);
            }

        }

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !alreadyScan && CanScan)
        {
            scannerWaitTimer += Time.deltaTime;
            UiObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, UiObject.transform.position.z);
            UiObject.transform.localScale = new Vector3(scannerWaitTimer *3, scannerWaitTimer *3, 1f);
            if (scannerWaitTimer > scannerWaitTime)
            {
                MainScanner.StartScan();
                alreadyScan = true;
                CanScan = false;
                UiObject.transform.localScale = new Vector3(0f, 0f, 1f);

            }
            // assign new position to where finger was pressed

        }
        else
        {
            scannerWaitTimer = 0;
            alreadyScan = false;
            UiObject.transform.localScale = new Vector3(0f, 0f, 1f);
        }
        if (Input.GetMouseButtonUp(0)) CanScan = true;

#endif

    }
}
