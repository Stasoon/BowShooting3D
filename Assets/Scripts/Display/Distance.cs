using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    TextMeshProUGUI textField;
    [SerializeField] Transform aimTransform;
    
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Camera.main.transform.position, aimTransform.position);
        textField.text = $"{System.Math.Round(dist, 2)} м";
    }
}
