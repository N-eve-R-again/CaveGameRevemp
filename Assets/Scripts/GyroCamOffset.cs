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
    public Vector3 targetOffset;
    public float GyroForce;
    public float MaxOffset;

    public bool targetMov;
    public Transform targetTransform;

    void Start()
    {
        //Set up and enable the gyroscope (check your device has one)
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
        //UpdatePosition(transform);
        
    }
    
    public void UpdatePosition(Transform NewPos)
    {
        baseRot = NewPos.rotation.eulerAngles;
        transform.position = NewPos.position;
        ApplyRot = baseRot;
        transform.rotation = Quaternion.Euler(ApplyRot);
        UserMovement = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //UserMovement = m_Gyro.userAcceleration;
        //UserMovement = m_Gyro.userAcceleration * Time.deltaTime * GyroForce;

        if (targetMov)
        {
            transform.position = targetTransform.position - targetOffset;
        }
        else
        {
            ApplyRot.x = Mathf.Clamp(baseRot.x + UserMovement.y * GyroForce, baseRot.x - MaxOffset, baseRot.x + MaxOffset);
            ApplyRot.y = Mathf.Clamp(baseRot.y + UserMovement.x * GyroForce, baseRot.y - MaxOffset, baseRot.y + MaxOffset);
            //ApplyRot.z = Mathf.Clamp(ApplyRot.z + UserMovement.z, baseRot.z - 10f, baseRot.z + 10f);

            //transform.Rotate(ApplyRot.x, ApplyRot.y, ApplyRot.z);

            transform.rotation = Quaternion.Euler(ApplyRot);
        }

        
    }
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    /*void OnGUI()
    {
        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        GUI.Label(new Rect(500, 500, 200, 40), "accereration : " + m_Gyro.userAcceleration);
    }*/
}
