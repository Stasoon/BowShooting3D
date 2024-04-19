using System;
using System.Collections;
using UnityEngine;


public class Bow : MonoBehaviour
{
    private KeyCode aimButton = KeyCode.Mouse1;

    [Header("Параметры лука")]
    [SerializeField] private float _tension = 0;
    public readonly float maxTension = 0.4f;
    public event Action<float> OnTensionChanged;
    public float Tension {
        get {return _tension;}
        private set {_tension = value; OnTensionChanged?.Invoke(_tension);}
    }

    [Header("Верёвка")]
    [SerializeField] private Transform ropeTransform;
    private Vector3 ropeStartPosition;
    
    [Header("Стрела")]
    [SerializeField] GameObject arrowPrefab; 
    [SerializeField] float arrowSpeed = 40f;
    BowArrow arrow;
    Transform arrowTransform;

    [Header("Анимации")]
    [SerializeField] AnimationCurve aimAnim;  
    [SerializeField] AnimationCurve reloadAnim;  

    [Header("Звуки")]
    //[SerializeField] AudioClip tensionAudioClip;
    [SerializeField] AudioClip whistlingAudioClip;
    AudioSource bowAudioSource;

    float aimSpeed = 1f;

    bool isAiming = false;

    private void Start() {
        ropeStartPosition = ropeTransform.localPosition;

        bowAudioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.GetKeyDown(aimButton)) {
            isAiming = true;

            GameObject newArrow = Instantiate(arrowPrefab, ropeTransform.position, ropeTransform.rotation, transform);
            arrow = newArrow.GetComponent<BowArrow>();
            arrowTransform = newArrow.transform;
        }
        else if (Input.GetKeyUp(aimButton)) {
            bowAudioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            bowAudioSource.PlayOneShot(whistlingAudioClip);

            arrow.Shoot(Tension*arrowSpeed);

            isAiming = false;
            Tension = 0;
            arrow = null;
            StartCoroutine(ReloadRope());
        }

        if (isAiming) {
            Aim();
        }
    }

    private void Aim() {
        // �������� ��������� 
        float progress = Tension / maxTension;
        Tension += aimAnim.Evaluate(progress) / 100 * aimSpeed;

        if (Tension > maxTension) {
            Tension = maxTension;
        }

        Vector3 newRopePosition = ropeTransform.localPosition;
        newRopePosition.x = ropeStartPosition.x + Tension;
        ropeTransform.localPosition = newRopePosition;

        arrowTransform.localPosition = ropeTransform.localPosition;
    }

    IEnumerator ReloadRope() {
        Vector3 posOnShot = ropeTransform.localPosition;
        float returnSpeed = 3f;

        for (float f = 0; f <= 1f; f += Time.fixedDeltaTime) {
            ropeTransform.localPosition = Vector3.LerpUnclamped(posOnShot, ropeStartPosition, reloadAnim.Evaluate(f * returnSpeed));
            yield return null;
        }

        ropeTransform.localPosition = ropeStartPosition;
    }
}
