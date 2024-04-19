using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float sensivity = 2.0f;
    float maxYAngle = 90.0f;

    float rotationX, rotationY;
    [SerializeField] float moveSpeed = 1f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //transform.Rotate(Vector3.up * mouseX * sensivity);

        rotationX -= mouseY * sensivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        rotationY += mouseX * sensivity;

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
    }
}
