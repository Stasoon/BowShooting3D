using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float sensivity = 1f;
    float maxYAngle = 90.0f;

    float rotationX, rotationY;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //transform.Rotate(Vector3.up * mouseX * sensivity);
        
        rotationX -= mouseY * sensivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        rotationY += mouseX * sensivity;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
        //transform.rotation = Quaternion.Euler(0f, Input.GetAxis("Mouse Y"), 0.0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
        // //transform.Rotate(Vector3.up * mouseX * sensivity);

        // rotationX -= mouseY * sensivity;
        // rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        // rotationY += mouseX * sensivity;

        // transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
    }
}