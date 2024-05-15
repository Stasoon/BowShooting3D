using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindDirectionDisplay : MonoBehaviour
{
    [SerializeField] RectTransform windDirectionArrow;
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        Preferences pref = Camera.main.GetComponent<Preferences>();
        Vector3 windDir = pref.windDirection;

        text.text = $"{pref.windSpeed} м/c";

        float angle = Mathf.Atan2(windDir.z, windDir.x) * Mathf.Rad2Deg;

        windDirectionArrow.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
