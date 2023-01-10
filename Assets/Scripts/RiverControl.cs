using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed;
    public float hurtSpeed;
    public float accelerationSpeed;
    public float controlSpeed;
    public float stunTimer;
    public float dir;
    public GyroCamOffset cameraManager;
    public GameManager gameManager;
    public Rigidbody rb;
    RiverUiManager riverUi;

    void Start()
    {
        riverUi = FindObjectOfType<RiverUiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stunTimer < 0f)
        {
            rb.AddForce(Vector3.forward * accelerationSpeed);

            rb.velocity = new Vector3(dir * controlSpeed, 0f ,Mathf.Clamp(rb.velocity.z,-10f,maxSpeed));

            if(Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width * 0.3f)
                {
                    dir = Mathf.Lerp(dir, -1f, Time.deltaTime);
                }
                else if (Input.mousePosition.x > Screen.width * 0.6f)
                {
                    dir = Mathf.Lerp(dir, 1f, Time.deltaTime);
                }
                else
                {
                    dir = Mathf.Lerp(dir, 0f, Time.deltaTime);
                }
                
            }
            else
            {
                dir = Mathf.Lerp(dir, 0f, Time.deltaTime);
            }
            cameraManager.shakeOffset = Vector3.zero;
        }
        else
        {
            stunTimer -= Time.deltaTime;
            cameraManager.Shake();
            riverUi.Hit();
            AudioManager.instance.PlayOS(Random.Range(5, 7));
            cameraManager.shakeForce = Mathf.Clamp((stunTimer -0.5f) * 0.2f,0f,1f);
        }


        riverUi.UpdateSlider(dir);
        


    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.Normalize(transform.position - collision.GetContact(0).point) * hurtSpeed;
        stunTimer = 1f;
        dir = Vector3.Normalize(transform.position - collision.GetContact(0).point).x;
        if(gameManager.mscanner.scanPower > 0)
        {
            gameManager.mscanner.scanPower -= 1;
            gameManager.uiscanner.UpdatePowerBar(gameManager.mscanner.scanPower);
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("end");
        gameManager.dir = 1;
        gameManager.StartFade();


    }
}
