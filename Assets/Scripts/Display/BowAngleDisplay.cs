using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BowAngleDisplay : MonoBehaviour
{
    [SerializeField] Transform bowTransform;
    TextMeshProUGUI textField;

    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        Vector3 bowUpDirection = bowTransform.up;
        float angle = Mathf.Atan2(bowUpDirection.x, bowUpDirection.y) * Mathf.Rad2Deg;

        textField.text = $"{Convert.ToInt32(angle)}°";
    }
}
