using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static InfoPing;

public class UiScanner : MonoBehaviour
{
    public float scannerWaitTime;
    public float scannerWaitTimer;
    LayerMask UILayer;
    public bool CanScan;
    public bool OverrideScan;
    public bool ValidScan;
    public GameObject UiObject;
    public scanner MainScanner;
    public GyroCamOffset CameraOffset;
    public InfoPing lastPingSelected;
    public bool alreadyScan;
    public ParticleSystem hitEffect;
    public ParticleSystem hitEffectWater;
    public Slider powerBar;
    float targetPower;

    void Start()
    {
        UiObject.transform.localScale = new Vector3(0f, 0f, 1f);
        targetPower = 10f;
        UILayer = LayerMask.NameToLayer("UI");
    }

    // Update is called once per frame
    void Update()
    {
        powerBar.value = Mathf.Lerp(powerBar.value,targetPower, Time.deltaTime *2);

        //if(Input.mousePosition.x >, Input.mousePosition.y){
        //Debug.Log(new Vector2(Input.mousePosition.x , Input.mousePosition.y));

        //}
        if (Input.mousePosition.y > Screen.height * 0.9f && Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width * 0.05f)
        {
            ValidScan= false;
        }
        else
        {
            ValidScan = true;
        }

        if (Input.GetMouseButton(0) && !alreadyScan && CanScan && !OverrideScan && ValidScan)
        {


            if (MainScanner.currentState == scanner.scanState.Broken)
            {
                scannerWaitTimer += Time.deltaTime * 2f;
                
                AudioManager.instance.PlayOS(1);

            }
            else
            {
                scannerWaitTimer += Time.deltaTime;
                AudioManager.instance.PlayOS(0);

            }

            UiObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, UiObject.transform.position.z);

            //Vector3 temp = new Vector3((Input.mousePosition.x - Screen.width / 2) / Screen.width, -(Input.mousePosition.y - Screen.height / 2) / Screen.height, 0f);

            //CameraOffset.UserMovement = Vector3.Lerp(CameraOffset.UserMovement, temp, Time.deltaTime);

            Vector3 temp = new Vector3((Input.mousePosition.x - Screen.width / 2) / Screen.width, -(Input.mousePosition.y - Screen.height / 2) / Screen.height, 0f);
            CameraOffset.UserMovement = Vector3.Lerp(CameraOffset.UserMovement, temp, Time.deltaTime);



            UiObject.transform.localScale = new Vector3(scannerWaitTimer * 3 + 0.5f, scannerWaitTimer * 3 + 0.5f, 1f);
            if (scannerWaitTimer > scannerWaitTime)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit2;

                if (Physics.Raycast(ray, out hit2))
                {
                    if(hit2.transform.gameObject.layer == UILayer)
                    {
                        
                    }
                    if (hit2.transform.tag == "InfoPing")
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
                            
                            OverrideScan = true;

                        }
                        lastPingSelected.ShowInfo();
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
                    }else if (hit2.transform.tag == "Cristal")
                    {
                        Cristal cristaltemp = hit2.transform.GetComponentInParent<Cristal>();
                        cristaltemp.Recolt();
                        scannerWaitTimer = 0;
                        CanScan = false;
                        AudioManager.instance.PlayOS(7);
                    }
                    else if (hit2.transform.tag == "Respawn")
                    {
                        EndArrow endtemp = hit2.transform.GetComponentInParent<EndArrow>();
                        endtemp.UseArrow();
                    }
                    else
                    {
                        //AudioManager.instance.FadeOS(0);
                        
                        alreadyScan = true;
                        CanScan = false;
                        MainScanner.StartScan();
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
                //AudioManager.instance.FadeOS(1);
            }
            else
            {

            }
            alreadyScan = false;
            UiObject.transform.localScale = new Vector3(0f, 0f, 1f);
            ///

            CameraOffset.UserMovement = Vector3.Lerp(CameraOffset.UserMovement, Vector3.zero, Time.deltaTime / 3f);

        }
        if (Input.GetMouseButtonUp(0) || !ValidScan) 
        {
            CanScan = true;
            AudioManager.instance.FadeOS(0);
            AudioManager.instance.FadeOS(1);
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

    public void UpdatePowerBar(int power)
    {
        powerBar.fillRect.GetComponent<Image>().color = Color.white;
        targetPower = power;
        if (power <= 0)
        {
            powerBar.fillRect.GetComponent<Image>().color= Color.red;

        }
    }
}
