using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndArrow : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        Playercamera = FindObjectOfType<Camera>().gameObject;
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

        arrowUi.SetActive(revealed);

                arrowUi.GetComponent<MeshRenderer>().material.color = Color.yellow;

        

    }
    public void UseArrow()
    {
        Debug.Log("ARROW CLICK");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
