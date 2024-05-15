using System;
using UnityEngine;

public class Preferences : MonoBehaviour 
{
    public Vector3 windDirection; // Направление ветра
    public float windSpeed = 5f; // Скорость ветра
    [HideInInspector] public Vector3 windForce {
        get {return windDirection * windSpeed;}
        set {}
    }

    public float G = 9.81f;
    [HideInInspector] public Vector3 g;

    float _arrowSteadyStateSpeed;
    public event Action<float> onArrowTypeChanged;
    public float ArrowSteadyStateSpeed {
        get {return _arrowSteadyStateSpeed;}
        private set {_arrowSteadyStateSpeed = value; onArrowTypeChanged?.Invoke(_arrowSteadyStateSpeed);}
    }

    void Awake() {
        windDirection = windDirection.normalized;
        windForce = windDirection * windSpeed;
        g = new Vector3(0, -G, 0);
    }

    void Start() {
        ArrowSteadyStateSpeed = 60f;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ArrowSteadyStateSpeed = 60;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ArrowSteadyStateSpeed = 80;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ArrowSteadyStateSpeed = 100;
        }
    }
}
