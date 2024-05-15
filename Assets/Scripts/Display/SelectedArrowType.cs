using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedArrowType : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        Preferences pref = Camera.main.GetComponent<Preferences>();
        pref.onArrowTypeChanged += ChangeSelectedArrowType;

        text = GetComponent<TextMeshProUGUI>();
    }

    void ChangeSelectedArrowType(float newSpeed) {
        text.text = $"Vуст = {newSpeed} м/с";
    }
}
