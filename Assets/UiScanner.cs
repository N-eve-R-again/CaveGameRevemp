using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class UiScanner : MonoBehaviour
{
    public float scannerWaitTime;
    public float scannerWaitTimer;

    public bool CanScan;
    public bool OverrideScan;
    public GameObject UiObject;
    public scanner MainScanner;
    public GyroCamOffset CameraOffset;
    public InfoPing lastPingSelected;
    public bool alreadyScan;
    public ParticleSystem hitEffect;
    void Start()
    {
        UiObject.transform.localScale = new Vector3(0f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {


        for (var i = 0; i < Input.touchCount; i++)
        {
            CameraOffset.UserMovement = Input.GetTouch(i).position;
            //Debug.Log(Input.GetTouch(i).phase);
            if (Input.GetTouch(i).phase != TouchPhase.Ended && CanScan)
            {
                scannerWaitTimer += Time.deltaTime;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                UiObject.transform.localScale = new Vector3(scannerWaitTimer * 3 + 0.5f, scannerWaitTimer * 3, 1f);
                UiObject.transform.position = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, UiObject.transform.position.z);
                
                if (scannerWaitTimer > scannerWaitTime)
                {
                    MainScanner.StartScan();
                    alreadyScan = true;
                    CanScan = false;
                    UiObject.transform.localScale = new Vector3(0f, 0f, 1f);

                }

                // Create a particle if hit
                if (Physics.Raycast(ray,out hit))
                {
                    Debug.Log(hit.transform.name);
                    Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAA");
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
        if (Input.GetMouseButton(0) && !alreadyScan && CanScan && !OverrideScan)
        {
            scannerWaitTimer += Time.deltaTime;
            UiObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, UiObject.transform.position.z);

            Vector3 temp = new Vector3((Input.mousePosition.x - Screen.width / 2) / Screen.width, -(Input.mousePosition.y - Screen.height / 2) / Screen.height, 0f);
            CameraOffset.UserMovement = Vector3.Lerp(CameraOffset.UserMovement, temp, Time.deltaTime);
            UiObject.transform.localScale = new Vector3(scannerWaitTimer * 3 + 0.5f, scannerWaitTimer * 3 + 0.5f, 1f);
            if (scannerWaitTimer > scannerWaitTime)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit2;

                // Create a particle if hit
                if (Physics.Raycast(ray, out hit2))
                {
                    //Debug.Log(hit.transform.name);
                    if(hit2.transform.tag == "InfoPing")
                    {
                        lastPingSelected = hit2.transform.GetComponentInParent<InfoPing>();
                        lastPingSelected.ShowInfo();
                        OverrideScan = true;
                        scannerWaitTimer = 0;
                        CanScan = false;
                    }
                    else
                    {
                        MainScanner.StartScan();
                        alreadyScan = true;
                        CanScan = false;
                    }



                }


                UiObject.transform.localScale = new Vector3(0f, 0f, 1f);

            }


            //Debug.Log(Physics.Raycast(ray));

            // assign new position to where finger was pressed

        }
        else
        {
            scannerWaitTimer = 0;
            alreadyScan = false;
            UiObject.transform.localScale = new Vector3(0f, 0f, 1f);
            if (Input.GetMouseButton(0))
            {
                Vector3 temp = new Vector3((Input.mousePosition.x - Screen.width / 2) / Screen.width, -(Input.mousePosition.y - Screen.height / 2) / Screen.height, 0f);
                CameraOffset.UserMovement = Vector3.Lerp(CameraOffset.UserMovement, temp, Time.deltaTime);
            }
            else
            {
                CameraOffset.UserMovement = Vector3.Lerp(CameraOffset.UserMovement, Vector3.zero, Time.deltaTime / 5f);
            }


        }
        if (Input.GetMouseButtonUp(0)) 
        {
            CanScan = true;

        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                hitEffect.transform.position = hit.point;
                hitEffect.Play();
            }

        }

#endif

    }
}
