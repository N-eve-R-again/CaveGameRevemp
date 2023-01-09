using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanner : MonoBehaviour
{
    public Material mat;
    public float currentoffset;
    public float timer;
    public float speed;
    public float normalSpeed;
    public float brokenSpeed;
    public float speedQueue;
    public bool scanning;
    public bool queueScanning;


    public bool changeStateStunned;
    public int scanPower = 5;

    public scanState currentState = scanState.Normal;
    public enum scanState{
        Normal,
        Water,
        Broken
    }
    // Start is called before the first frame update
    void Start()
    {
        mat.SetFloat("_Offset", 9f);
        mat.SetFloat("_State", 0f);

    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonDown(0))
        {
            StartScan();
        }*/
        if(timer < 1f)
        {
            if (queueScanning)
            {
                timer += Time.deltaTime * speedQueue;
            }
            else
            {
                timer += Time.deltaTime * speed;
            }

            currentoffset = Mathf.Lerp(-0f, 6f, timer);

            mat.SetFloat("_Offset", currentoffset);
        }
        else
        {
            scanning = false;
            //Debug.Log("quesquecest");
            if (queueScanning)
            {
                //AudioManager.instance.FadeOS(1);

                //AudioManager.instance.PlayOS(0);
                AudioManager.instance.FadeOS(2);
                AudioManager.instance.FadeOS(3);
                queueScanning = false;
                if(!changeStateStunned)
                {
                    StartScan();
                    

                }
                else
                {
                    changeStateStunned = false;
                    mat.SetFloat("_State", 0f);
                    speed = brokenSpeed;
                }


            }

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Recharge();
        }



    }
    
    public void Recharge()
    {
        scanPower += 5;
        scanPower = Mathf.Clamp(scanPower, 0, 10);
        currentState = scanState.Normal;
        gameObject.GetComponent<UiScanner>().UpdatePowerBar(scanPower);
    }
    public void StartScan()
    {
        if (FindObjectOfType<MenuManager>().tutorialScanAvailable)
        {
            FindObjectOfType<MenuManager>().InitMenu();
        }

        gameObject.GetComponent<UiScanner>().UpdatePowerBar(scanPower);
        if (scanning)
        {
            queueScanning = true;
            if (scanPower == 0)
            {
                currentState = scanState.Broken;
                AudioManager.instance.PlayOS(4);
                changeStateStunned = true;
                scanPower = -1;

            }
        }
        else
        {
            if (scanPower == 0)
            {
                currentState = scanState.Broken;
                AudioManager.instance.PlayOS(4);
                scanPower = -1;


            }
            else 
            {
                if (scanPower > -1)
                {
                    scanPower -= 1;
                }

                scanning = true;
                timer = 0;

            }
            switch (currentState)
            {
                case scanState.Normal:
                    mat.SetFloat("_State", 1f);
                    speed = normalSpeed;
                    AudioManager.instance.PlayOS(2);
                    break;
                case scanState.Water:
                    break;
                case scanState.Broken:
                    mat.SetFloat("_State", 0f);
                    AudioManager.instance.PlayOS(3);
                    speed = brokenSpeed;
                    break;
                    
            }



            //Debug.Log("AAAAAAAAAAAA");

        }

    }
}
