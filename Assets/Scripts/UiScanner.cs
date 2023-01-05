using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using static InfoPing;

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
    public ParticleSystem hitEffectWater;
    void Start()
    {
        UiObject.transform.localScale = new Vector3(0f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButton(0) && !alreadyScan && CanScan && !OverrideScan)
        {
            if(MainScanner.currentState == scanner.scanState.Broken)
            {
                scannerWaitTimer += Time.deltaTime * 3f;
            }
            else
            {
                scannerWaitTimer += Time.deltaTime;
            }
            UiObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, UiObject.transform.position.z);

            Vector3 temp = new Vector3((Input.mousePosition.x - Screen.width / 2) / Screen.width, -(Input.mousePosition.y - Screen.height / 2) / Screen.height, 0f);
            CameraOffset.UserMovement = Vector3.Lerp(CameraOffset.UserMovement, temp, Time.deltaTime);
            UiObject.transform.localScale = new Vector3(scannerWaitTimer * 3 + 0.5f, scannerWaitTimer * 3 + 0.5f, 1f);
            if (scannerWaitTimer > scannerWaitTime)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit2;

                if (Physics.Raycast(ray, out hit2))
                {
                    if(hit2.transform.tag == "InfoPing")
                    {

                        lastPingSelected = hit2.transform.GetComponentInParent<InfoPing>();

                        if (MainScanner.currentState == scanner.scanState.Broken)
                        {
                            if (lastPingSelected.currentState != infoState.Saved)
                            {
                                lastPingSelected.currentState = infoState.Corrupted;
                            }

                        }
                        else
                        {
                            lastPingSelected.currentState = infoState.Saved;
                            lastPingSelected.ShowInfo();
                            OverrideScan = true;

                        }
                        scannerWaitTimer = 0;
                        CanScan = false;

                    }
                    else if (hit2.transform.tag == "MoveArrow")
                    {
                        MoveArrow arrowtemp = hit2.transform.GetComponentInParent<MoveArrow>();
                        arrowtemp.UseArrow();

                        arrowtemp.currentState = MoveArrow.infoState.Saved;
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
        }
        else
        {
            scannerWaitTimer = 0;
            if(MainScanner.currentState == scanner.scanState.Broken)
            {
                scannerWaitTimer = 0.2f;
            }
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
                if (hit.transform.tag != "InfoPing" && hit.transform.tag != "MoveArrow")
                {
                    if(hit.transform.tag == "Water")
                    {
                        hitEffectWater.transform.position = hit.point + new Vector3(0f,0.05f,0f);
                        hitEffectWater.Play();
                    }
                    else
                    {
                        hitEffect.transform.position = hit.point;
                        hitEffect.Play();
                    }

                }

            }

        }

    }
}
