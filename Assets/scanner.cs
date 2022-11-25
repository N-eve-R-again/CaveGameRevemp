using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scanner : MonoBehaviour
{
    public Material mat;
    public float currentoffset;
    public float timer;
    public float speed;
    public float speedQueue;
    public bool scanning;
    public bool queueScanning;
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
                timer = 0;
                scanning = true;
                queueScanning = false;
            }
        }
    }

    public void StartScan()
    {
        if (scanning)
        {
            queueScanning = true;
        }
        else
        {
            timer = 0;
            scanning = true;
            Debug.Log("AAAAAAAAAAAA");
        }

    }
}
