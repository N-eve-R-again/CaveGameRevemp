using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GyroCamOffset : MonoBehaviour
{
    Gyroscope m_Gyro;
    public Vector3 baseRot;
    public Vector3 UserMovement;
    public Vector3 ApplyRot;
    public float GyroForce;
    public float MaxOffset;

    void Start()
    {
        //Set up and enable the gyroscope (check your device has one)
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
        baseRot = transform.rotation.eulerAngles;
        ApplyRot = baseRot;
        
    }

    // Update is called once per frame
    void Update()
    {
        //UserMovement = m_Gyro.userAcceleration;
        UserMovement = m_Gyro.userAcceleration * Time.deltaTime * GyroForce;

        ApplyRot.x = Mathf.Clamp(ApplyRot.x + UserMovement.y, baseRot.x - MaxOffset, baseRot.x + MaxOffset);
        ApplyRot.y = Mathf.Clamp(ApplyRot.y + UserMovement.x, baseRot.y - MaxOffset, baseRot.y + MaxOffset);
        //ApplyRot.z = Mathf.Clamp(ApplyRot.z + UserMovement.z, baseRot.z - 10f, baseRot.z + 10f);

        //transform.Rotate(ApplyRot.x, ApplyRot.y, ApplyRot.z);

        transform.rotation = Quaternion.Euler(ApplyRot);
        //float temp = /*Mathf.Clamp(*/transform.eulerAngles.x + UserMovement.x; /*, -10f, 10f);*/
        //Debug.Log(temp);
        //transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.eulerAngles.x + UserMovement.x, -10f, 10f), Mathf.Clamp(transform.eulerAngles.y + UserMovement.y, -10f, 10f), Mathf.Clamp(transform.eulerAngles.z + UserMovement.z, -10f, 10f));

    }
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    void OnGUI()
    {
        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        GUI.Label(new Rect(500, 500, 200, 40), "accereration : " + m_Gyro.userAcceleration);
    }
}
