using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    public float minOffset;
    public float maxOffset;
    public bool revealed;

    public scanner MainScanner;
    public GameObject Mesh;
    public ParticleSystem Particles;
    public GameObject Playercamera;
    public float distance;
    public bool recolted;
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
        if (recolted)
        {
            revealed = false;
        }
        Mesh.SetActive(revealed);




    }
    public void Recolt()
    {

        FindObjectOfType<scanner>().Recharge();

            Particles.Play();

            recolted = true;
        
        
        

    }
}
