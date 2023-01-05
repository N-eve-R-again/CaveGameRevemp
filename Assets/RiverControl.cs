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
    public Rigidbody rb;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stunTimer < 0f)
        {
            rb.AddForce(Vector3.forward * accelerationSpeed);

            rb.velocity = new Vector3(dir * controlSpeed, 0f ,Mathf.Clamp(rb.velocity.z,-10f,maxSpeed));

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                dir = Mathf.Lerp(dir, -1f, Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
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
            stunTimer -= Time.deltaTime;
        }





    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.Normalize(transform.position - collision.GetContact(0).point) * hurtSpeed;
        stunTimer = 1f;
        dir = Vector3.Normalize(transform.position - collision.GetContact(0).point).x;

    }
}
