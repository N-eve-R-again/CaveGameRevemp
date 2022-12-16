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
        mat.SetFloat("_Offset", 9);
#if UNITY_EDITOR
        //mat.SetInt("_EditorMode", 1);
#endif
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

            currentoffset = Mathf.Lerp(-0f, 8f, timer);

            mat.SetFloat("_Offset", currentoffset);
        }
        else
        {
            scanning = false;
            if(queueScanning)
            {
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
            scanPower = 5;
            currentState = scanState.Normal;
        }


    }

    public void StartScan()
    {
        if (scanning)
        {
            queueScanning = true;
            if (scanPower == 0)
            {
                currentState = scanState.Broken;
                changeStateStunned = true;
                scanPower = -1;

            }
        }
        else
        {
            switch (currentState)
            {
                case scanState.Normal:
                    mat.SetFloat("_State", 1f);
                    speed = normalSpeed;
                    break;
                case scanState.Water:
                    break;
                case scanState.Broken:
                    mat.SetFloat("_State", 0f);
                    speed = brokenSpeed;
                    break;

            }
            if (scanPower <= 0)
            {
                currentState = scanState.Broken;


            }
            else
            {
                scanPower -= 1;
            }
            timer = 0;
            scanning = true;
            //Debug.Log("AAAAAAAAAAAA");

        }

    }
}